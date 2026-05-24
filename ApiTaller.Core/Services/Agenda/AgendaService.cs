using ApiTaller.Domain.Dtos.Agenda;
using ApiTaller.Domain.Interfaces.Repositories.Agenda;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.Agenda;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Agenda
{
    public class AgendaService : IAgendaService
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<AgendaService> _logger;

        public AgendaService(
            IAgendaRepository agendaRepository,
            ICurrentUserService currentUserService,
            ILogger<AgendaService> logger)
        {
            _agendaRepository = agendaRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<AgendaSettingsDto?> GetSettingsAsync(CancellationToken ct)
        {
            try
            {
                return await _agendaRepository.GetSettingsAsync(ct);
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
                return await _agendaRepository.UpdateSettingsAsync(dto, ct);
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
                return await _agendaRepository.AddBlockDateAsync(dto, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al bloquear fecha de agenda");
                return false;
            }
        }

        public async Task<IEnumerable<string>> GetAvailableDatesAsync(CancellationToken ct)
        {
            try
            {
                return await _agendaRepository.GetAvailableDatesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fechas disponibles");
                return new List<string>();
            }
        }

        public async Task<bool> BookAsync(BookAppointmentDto dto, CancellationToken ct)
        {
            try
            {
                if (!int.TryParse(_currentUserService.UserId, out int userId))
                {
                    _logger.LogWarning("BookAsync: No se pudo resolver el ID del usuario actual");
                    return false;
                }

                // REGLA DE NEGOCIO: No permitir si el vehículo ya tiene cita activa
                bool hasActive = await _agendaRepository.HasActiveAppointmentForVehicleAsync(dto.VehicleId, ct);
                if (hasActive)
                {
                    _logger.LogWarning("El vehículo {VehicleId} ya tiene una cita activa. No se puede agendar.", dto.VehicleId);
                    return false;
                }

                return await _agendaRepository.BookAsync(dto, userId, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reservar cita para vehículo {VehicleId}", dto.VehicleId);
                return false;
            }
        }

        public async Task<bool> PreRegisterAsync(PreRegisterAppointmentDto dto, CancellationToken ct)
        {
            try
            {
                return await _agendaRepository.PreRegisterAsync(dto, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al pre-registrar cita");
                return false;
            }
        }

        public async Task<bool> AdminBookAsync(AdminBookAppointmentDto dto, CancellationToken ct)
        {
            try
            {
                int? responsibleUserId = null;
                if (int.TryParse(_currentUserService.UserId, out int userId))
                    responsibleUserId = userId;

                // REGLA DE NEGOCIO: No permitir si el vehículo ya tiene cita activa
                if (dto.VehicleId.HasValue)
                {
                    bool hasActive = await _agendaRepository.HasActiveAppointmentForVehicleAsync(dto.VehicleId.Value, ct);
                    if (hasActive)
                    {
                        _logger.LogWarning("AdminBookAsync: El vehículo {VehicleId} ya tiene una cita activa.", dto.VehicleId);
                        return false;
                    }
                }

                return await _agendaRepository.AdminBookAsync(dto, responsibleUserId, ct);
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
                return await _agendaRepository.GetDailyAsync(date, ct);
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
                return await _agendaRepository.ConfirmPreRegisterAsync(dto, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al confirmar pre-registro de cita {Id}", dto.AppointmentId);
                return false;
            }
        }

        public async Task<int?> ConvertToWorkOrderAsync(int appointmentId, CancellationToken ct)
        {
            try
            {
                if (!int.TryParse(_currentUserService.UserId, out int responsibleUserId))
                {
                    _logger.LogWarning("ConvertToWorkOrderAsync: No se pudo resolver el usuario responsable");
                    return null;
                }

                return await _agendaRepository.ConvertToWorkOrderAsync(appointmentId, responsibleUserId, ct);
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
                return await _agendaRepository.GetDayConfigsAsync(weeks, start, ct);
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
                return await _agendaRepository.UpdateDayConfigAsync(dto, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar configuración diaria del día {Date}", dto.Date);
                return false;
            }
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId, CancellationToken ct)
        {
            try
            {
                return await _agendaRepository.CancelAppointmentAsync(appointmentId, ct);
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
                return await _agendaRepository.RescheduleAsync(appointmentId, newDate, ct);
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
                return await _agendaRepository.GetBlockedExceptionDatesAsync(ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener excepciones de bloqueo");
                return new List<AgendaBlockDto>();
            }
        }

        public async Task<bool> DeleteBlockedExceptionDateAsync(int id, CancellationToken ct)
        {
            try
            {
                return await _agendaRepository.DeleteBlockedExceptionDateAsync(id, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar excepción de bloqueo {Id}", id);
                return false;
            }
        }
    }
}
