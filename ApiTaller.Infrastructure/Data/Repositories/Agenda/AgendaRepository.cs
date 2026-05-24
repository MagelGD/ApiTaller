using ApiTaller.Domain.Dtos.Agenda;
using ApiTaller.Domain.Interfaces.Repositories.Agenda;
using ApiTaller.Domain.Models;
using ApiTaller.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Agenda
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<AgendaRepository> _logger;
        private readonly ICurrentUserService _currentUser;

        public AgendaRepository(DataContext context, ILogger<AgendaRepository> logger, ICurrentUserService currentUser)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<AgendaSettingsDto?> GetSettingsAsync(CancellationToken ct)
        {
            try
            {
                var settings = await _context.AgendaSettings.FirstOrDefaultAsync(ct);
                if (settings == null) return null;

                return new AgendaSettingsDto
                {
                    Id = settings.Id,
                    WeeksToOpen = settings.WeeksToOpen,
                    DailySlots = settings.DailySlots,
                    BusinessHoursStart = settings.BusinessHoursStart.ToString(@"hh\:mm"),
                    BusinessHoursEnd = settings.BusinessHoursEnd.ToString(@"hh\:mm"),
                    StartDate = settings.StartDate,
                    WorkingDays = settings.WorkingDays
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener configuración de agenda");
                return null;
            }
        }

        public async Task<bool> UpdateSettingsAsync(AgendaSettingsDto dto, CancellationToken ct)
        {
            try
            {
                var settings = await _context.AgendaSettings.FirstOrDefaultAsync(ct);
                int.TryParse(_currentUser.UserId, out int userId);

                if (settings == null)
                {
                    settings = new AgendaSettings();
                    settings.CreatedAt = DateTime.Now;
                    settings.ResponsibleUserId = userId;
                    settings.IsActive = true;
                    await _context.AgendaSettings.AddAsync(settings, ct);
                }

                settings.WeeksToOpen = dto.WeeksToOpen;
                settings.DailySlots = dto.DailySlots;
                settings.BusinessHoursStart = TimeSpan.Parse(dto.BusinessHoursStart);
                settings.BusinessHoursEnd = TimeSpan.Parse(dto.BusinessHoursEnd);
                settings.StartDate = dto.StartDate.Date;
                settings.WorkingDays = dto.WorkingDays;
                settings.UpdatedAt = DateTime.Now;
                settings.ResponsibleUserId = userId;

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar configuración de agenda");
                return false;
            }
        }

        public async Task<bool> AddBlockDateAsync(AgendaBlockDto dto, CancellationToken ct)
        {
            try
            {
                int.TryParse(_currentUser.UserId, out int userId);
                var block = new AgendaBlock
                {
                    BlockDate = dto.BlockDate.Date,
                    Reason = dto.Reason,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                };

                await _context.AgendaBlock.AddAsync(block, ct);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al bloquear fecha de agenda");
                return false;
            }
        }

        public async Task<IEnumerable<DateTime>> GetBlockedDatesAsync(DateTime start, DateTime end, CancellationToken ct)
        {
            try
            {
                return await _context.AgendaBlock
                    .Where(b => b.IsActive && b.BlockDate >= start.Date && b.BlockDate <= end.Date)
                    .Select(b => b.BlockDate)
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fechas bloqueadas");
                return new List<DateTime>();
            }
        }

        public async Task<bool> HasActiveAppointmentForVehicleAsync(int vehicleId, CancellationToken ct)
        {
            try
            {
                return await _context.Appointment.AnyAsync(a =>
                    a.VehicleId == vehicleId &&
                    a.IsActive &&
                    (a.Status == "Agendada" || a.Status == "Pendiente"), ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar cita activa para vehículo {VehicleId}", vehicleId);
                return false;
            }
        }

        public async Task<IEnumerable<string>> GetAvailableDatesAsync(CancellationToken ct)
        {
            try
            {
                var settings = await _context.AgendaSettings.FirstOrDefaultAsync(ct);
                if (settings == null) return new List<string>();

                var today = DateTime.Today;
                var startDate = settings.StartDate.Date > today ? settings.StartDate.Date : today;
                var endDate = startDate.AddDays((settings.WeeksToOpen * 7) - 1);

                var dailyConfigs = await _context.AgendaDayConfig
                    .Where(c => c.Date >= startDate && c.Date <= endDate && c.IsActive)
                    .ToDictionaryAsync(c => c.Date.Date, c => c, ct);

                var appointmentsCountByDate = await _context.Appointment
                    .Where(a => a.IsActive && a.AppointmentDate >= startDate && a.AppointmentDate <= endDate
                           && (a.Status == "Agendada" || a.Status == "Pendiente"))
                    .GroupBy(a => a.AppointmentDate)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(g => g.Date, g => g.Count, ct);

                var blockedDates = await _context.AgendaBlock
                    .Where(b => b.IsActive && b.BlockDate >= startDate && b.BlockDate <= endDate)
                    .Select(b => b.BlockDate.Date)
                    .ToListAsync(ct);

                var workingDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
                if (!string.IsNullOrEmpty(settings.WorkingDays))
                {
                    workingDays = settings.WorkingDays.Split(',')
                        .Select(s => int.TryParse(s, out var d) ? (DayOfWeek)d : DayOfWeek.Sunday)
                        .ToList();
                }

                var availableDates = new List<string>();
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (!workingDays.Contains(date.DayOfWeek)) continue;
                    if (blockedDates.Contains(date.Date)) continue;

                    dailyConfigs.TryGetValue(date.Date, out var dayConfig);
                    if (dayConfig != null && dayConfig.IsBlocked) continue;

                    appointmentsCountByDate.TryGetValue(date.Date, out var count);
                    var slots = dayConfig?.CustomSlots ?? settings.DailySlots;

                    if (count < slots)
                        availableDates.Add(date.ToString("yyyy-MM-dd"));
                }

                return availableDates;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fechas disponibles");
                return new List<string>();
            }
        }

        public async Task<bool> BookAsync(BookAppointmentDto dto, int userId, CancellationToken ct)
        {
            try
            {
                var customer = await _context.Customer.FirstOrDefaultAsync(c => c.UserId == userId, ct);

                var appointment = new Appointment
                {
                    AppointmentDate = dto.AppointmentDate.Date,
                    CustomerId = customer?.Id,
                    VehicleId = dto.VehicleId,
                    ServiceTypeId = dto.ServiceTypeId,
                    Status = "Agendada",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId,
                    BookingSource = "Portal",
                    CustomerNotes = dto.CustomerNotes ?? "",
                    ContactName = "",
                    ContactPhone = "",
                    ContactEmail = "",
                    VehicleDescription = ""
                };

                await _context.Appointment.AddAsync(appointment, ct);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reservar cita");
                return false;
            }
        }

        public async Task<bool> PreRegisterAsync(PreRegisterAppointmentDto dto, CancellationToken ct)
        {
            try
            {
                int.TryParse(_currentUser.UserId, out int userId);
                var appointment = new Appointment
                {
                    AppointmentDate = dto.AppointmentDate.Date,
                    ContactName = dto.ContactName,
                    ContactPhone = dto.ContactPhone,
                    ContactEmail = dto.ContactEmail,
                    VehicleDescription = dto.VehicleDescription,
                    ServiceTypeId = dto.ServiceTypeId,
                    Status = "Pendiente",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId,
                    BookingSource = "Pre-registro",
                    CustomerNotes = dto.CustomerNotes ?? ""
                };

                await _context.Appointment.AddAsync(appointment, ct);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al pre-registrar cita");
                return false;
            }
        }

        public async Task<bool> AdminBookAsync(AdminBookAppointmentDto dto, int? responsibleUserId, CancellationToken ct)
        {
            try
            {
                int.TryParse(_currentUser.UserId, out int userId);
                var appointment = new Appointment
                {
                    AppointmentDate = dto.AppointmentDate.Date,
                    CustomerId = dto.CustomerId,
                    VehicleId = dto.VehicleId,
                    ServiceTypeId = dto.ServiceTypeId,
                    ContactName = dto.ContactName,
                    ContactPhone = dto.ContactPhone,
                    ContactEmail = dto.ContactEmail,
                    VehicleDescription = dto.VehicleDescription,
                    Status = "Agendada",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = responsibleUserId ?? userId,
                    BookingSource = "Admin",
                    CustomerNotes = dto.CustomerNotes ?? ""
                };

                await _context.Appointment.AddAsync(appointment, ct);
                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agendar cita (admin)");
                return false;
            }
        }

        public async Task<IEnumerable<AppointmentSummaryDto>> GetDailyAsync(DateTime date, CancellationToken ct)
        {
            try
            {
                return await _context.Appointment
                    .Include(a => a.VehicleNavigation)
                        .ThenInclude(v => v.CustomerNavigation)
                    .Include(a => a.VehicleNavigation)
                        .ThenInclude(v => v.BrandNavigation)
                    .Include(a => a.VehicleNavigation)
                        .ThenInclude(v => v.ModelNavigation)
                    .Include(a => a.ServiceTypeNavigation)
                    .Where(a => a.AppointmentDate.Date == date.Date && a.IsActive)
                    .Select(a => new AppointmentSummaryDto
                    {
                        Id = a.Id,
                        AppointmentDate = a.AppointmentDate,
                        Status = a.Status,
                        BookingSource = a.BookingSource,
                        ServiceTypeName = a.ServiceTypeNavigation != null ? a.ServiceTypeNavigation.Name : "Pre-registro",
                        CustomerName = a.VehicleNavigation != null ? $"{a.VehicleNavigation.CustomerNavigation.FirstName} {a.VehicleNavigation.CustomerNavigation.LastName}" : a.ContactName,
                        VehiclePlate = a.VehicleNavigation != null ? a.VehicleNavigation.Plate : "",
                        ContactName = a.ContactName,
                        ContactPhone = a.ContactPhone,
                        ContactEmail = a.ContactEmail,
                        VehicleDescription = a.VehicleNavigation != null ? $"{a.VehicleNavigation.BrandNavigation.Name} {a.VehicleNavigation.ModelNavigation.Models}" : a.VehicleDescription,
                        CustomerNotes = a.CustomerNotes,
                        WorkOrderId = a.WorkOrderId
                    })
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener citas del día {Date}", date.Date);
                return new List<AppointmentSummaryDto>();
            }
        }

        public async Task<bool> ConfirmPreRegisterAsync(ConfirmPreRegisterDto dto, CancellationToken ct)
        {
            try
            {
                var appointment = await _context.Appointment.FirstOrDefaultAsync(a => a.Id == dto.AppointmentId, ct);
                if (appointment == null) return false;

                int.TryParse(_currentUser.UserId, out int userId);
                appointment.CustomerId = dto.CustomerId;
                appointment.VehicleId = dto.VehicleId;
                appointment.Status = "Agendada";
                appointment.UpdatedAt = DateTime.Now;
                appointment.ResponsibleUserId = userId;

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al confirmar pre-registro de cita {Id}", dto.AppointmentId);
                return false;
            }
        }

        public async Task<int?> ConvertToWorkOrderAsync(int appointmentId, int responsibleUserId, CancellationToken ct)
        {
            try
            {
                var appointment = await _context.Appointment
                    .Include(a => a.VehicleNavigation)
                    .FirstOrDefaultAsync(a => a.Id == appointmentId, ct);

                if (appointment == null || appointment.VehicleId == null) return null;

                var workOrder = new WorkOrder
                {
                    VehicleId = appointment.VehicleId.Value,
                    CustomerId = appointment.VehicleNavigation.CustomerId,
                    EntryDate = DateTime.Now,
                    Status = "Recepción",
                    Observations = appointment.CustomerNotes,
                    Mileage = 0,
                    FuelLevel = "N/A",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ResponsibleUserId = responsibleUserId
                };

                _context.WorkOrder.Add(workOrder);
                await _context.SaveChangesAsync(ct);

                appointment.Status = "Recibida";
                appointment.WorkOrderId = workOrder.Id;
                appointment.UpdatedAt = DateTime.Now;
                appointment.ResponsibleUserId = responsibleUserId;

                await _context.SaveChangesAsync(ct);
                return workOrder.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al convertir cita {Id} a orden de trabajo", appointmentId);
                return null;
            }
        }

        public async Task<IEnumerable<AgendaDayConfigDto>> GetDayConfigsAsync(int? weeks, DateTime? start, CancellationToken ct)
        {
            try
            {
                var settings = await _context.AgendaSettings.FirstOrDefaultAsync(ct);

                if (settings == null && weeks == null) return new List<AgendaDayConfigDto>();

                var baseStartDate = start?.Date ?? settings?.StartDate.Date ?? DateTime.Today;
                var baseWeeks = weeks ?? settings?.WeeksToOpen ?? 2;

                var startDate = baseStartDate;
                var endDate = baseStartDate.AddDays((baseWeeks * 7) - 1);

                var dailyConfigs = await _context.AgendaDayConfig
                    .Where(c => c.Date >= startDate && c.Date <= endDate && c.IsActive)
                    .ToDictionaryAsync(c => c.Date.Date, c => c, ct);

                var blockedDates = await _context.AgendaBlock
                    .Where(b => b.IsActive && b.BlockDate >= startDate && b.BlockDate <= endDate)
                    .ToDictionaryAsync(b => b.BlockDate.Date, b => b.Reason, ct);

                var appointmentsCount = await _context.Appointment
                    .Where(a => a.IsActive && a.AppointmentDate >= startDate && a.AppointmentDate <= endDate
                           && (a.Status == "Agendada" || a.Status == "Pendiente"))
                    .GroupBy(a => a.AppointmentDate)
                    .Select(g => new { Date = g.Key, Count = g.Count() })
                    .ToDictionaryAsync(g => g.Date, g => g.Count, ct);

                var workingDays = new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday };
                if (settings != null && !string.IsNullOrEmpty(settings.WorkingDays))
                {
                    workingDays = settings.WorkingDays.Split(',')
                        .Select(s => int.TryParse(s, out var d) ? (DayOfWeek)d : DayOfWeek.Sunday)
                        .ToList();
                }

                var result = new List<AgendaDayConfigDto>();
                for (var date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    if (!workingDays.Contains(date.DayOfWeek)) continue;

                    dailyConfigs.TryGetValue(date.Date, out var config);
                    appointmentsCount.TryGetValue(date.Date, out var count);
                    blockedDates.TryGetValue(date.Date, out var blockReason);
                    var isBlockedInExceptions = blockReason != null;

                    result.Add(new AgendaDayConfigDto
                    {
                        Date = date,
                        CustomSlots = config?.CustomSlots,
                        IsBlocked = (config?.IsBlocked ?? false) || isBlockedInExceptions,
                        Reason = config?.Reason ?? blockReason,
                        CurrentBookings = count
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener configuraciones diarias de agenda");
                return new List<AgendaDayConfigDto>();
            }
        }

        public async Task<bool> UpdateDayConfigAsync(AgendaDayConfigDto dto, CancellationToken ct)
        {
            try
            {
                var config = await _context.AgendaDayConfig.FirstOrDefaultAsync(c => c.Date.Date == dto.Date.Date, ct);
                int.TryParse(_currentUser.UserId, out int userId);

                if (config == null)
                {
                    config = new AgendaDayConfig
                    {
                        Date = dto.Date.Date,
                        CustomSlots = dto.CustomSlots,
                        IsBlocked = dto.IsBlocked,
                        Reason = dto.Reason ?? "",
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    };
                    await _context.AgendaDayConfig.AddAsync(config, ct);
                }
                else
                {
                    config.CustomSlots = dto.CustomSlots;
                    config.IsBlocked = dto.IsBlocked;
                    config.Reason = dto.Reason ?? "";
                    config.UpdatedAt = DateTime.Now;
                    config.ResponsibleUserId = userId;
                }

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar configuración diaria de agenda");
                return false;
            }
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId, CancellationToken ct)
        {
            try
            {
                var appointment = await _context.Appointment.FirstOrDefaultAsync(a => a.Id == appointmentId, ct);
                if (appointment == null) return false;

                int.TryParse(_currentUser.UserId, out int userId);
                appointment.Status = "Cancelada";
                appointment.UpdatedAt = DateTime.Now;
                appointment.ResponsibleUserId = userId;

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cancelar cita {Id}", appointmentId);
                return false;
            }
        }

        public async Task<bool> RescheduleAsync(int appointmentId, DateTime newDate, CancellationToken ct)
        {
            try
            {
                var appointment = await _context.Appointment.FirstOrDefaultAsync(a => a.Id == appointmentId, ct);
                if (appointment == null) return false;

                int.TryParse(_currentUser.UserId, out int userId);
                appointment.AppointmentDate = newDate.Date;
                appointment.UpdatedAt = DateTime.Now;
                appointment.ResponsibleUserId = userId;

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reprogramar cita {Id}", appointmentId);
                return false;
            }
        }

        public async Task<IEnumerable<AgendaBlockDto>> GetBlockedExceptionDatesAsync(CancellationToken ct)
        {
            try
            {
                return await _context.AgendaBlock
                    .Where(b => b.IsActive && b.BlockDate >= DateTime.Today)
                    .OrderBy(b => b.BlockDate)
                    .Select(b => new AgendaBlockDto
                    {
                        Id = b.Id,
                        BlockDate = b.BlockDate,
                        Reason = b.Reason
                    })
                    .ToListAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener excepciones de bloqueo activas");
                return new List<AgendaBlockDto>();
            }
        }

        public async Task<bool> DeleteBlockedExceptionDateAsync(int id, CancellationToken ct)
        {
            try
            {
                var block = await _context.AgendaBlock.FirstOrDefaultAsync(b => b.Id == id, ct);
                if (block == null) return false;

                int.TryParse(_currentUser.UserId, out int userId);
                block.IsActive = false;
                block.UpdatedAt = DateTime.Now;
                block.ResponsibleUserId = userId;

                return await _context.SaveChangesAsync(ct) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar excepción de bloqueo {Id}", id);
                return false;
            }
        }
    }
}
