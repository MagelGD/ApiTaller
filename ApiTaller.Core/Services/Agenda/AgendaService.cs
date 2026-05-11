using ApiTaller.Domain.Dtos.Agenda;
using ApiTaller.Domain.Interfaces.Repositories.Agenda;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.Agenda;
using ApiTaller.Infrastructure.Security;
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

        public AgendaService(IAgendaRepository agendaRepository, ICurrentUserService currentUserService)
        {
            _agendaRepository = agendaRepository;
            _currentUserService = currentUserService;
        }

        public async Task<AgendaSettingsDto?> GetSettingsAsync(CancellationToken ct)
        {
            return await _agendaRepository.GetSettingsAsync(ct);
        }

        public async Task<bool> UpdateSettingsAsync(AgendaSettingsDto dto, CancellationToken ct)
        {
            return await _agendaRepository.UpdateSettingsAsync(dto, ct);
        }

        public async Task<bool> AddBlockDateAsync(AgendaBlockDto dto, CancellationToken ct)
        {
            return await _agendaRepository.AddBlockDateAsync(dto, ct);
        }

        public async Task<IEnumerable<string>> GetAvailableDatesAsync(CancellationToken ct)
        {
            return await _agendaRepository.GetAvailableDatesAsync(ct);
        }

        public async Task<bool> BookAsync(BookAppointmentDto dto, CancellationToken ct)
        {
            if (!int.TryParse(_currentUserService.UserId, out int userId))
                return false;

            return await _agendaRepository.BookAsync(dto, userId, ct);
        }

        public async Task<bool> PreRegisterAsync(PreRegisterAppointmentDto dto, CancellationToken ct)
        {
            return await _agendaRepository.PreRegisterAsync(dto, ct);
        }

        public async Task<bool> AdminBookAsync(AdminBookAppointmentDto dto, CancellationToken ct)
        {
            int? responsibleUserId = null;
            if (int.TryParse(_currentUserService.UserId, out int userId))
            {
                responsibleUserId = userId;
            }

            return await _agendaRepository.AdminBookAsync(dto, responsibleUserId, ct);
        }

        public async Task<IEnumerable<AppointmentSummaryDto>> GetDailyAsync(DateTime date, CancellationToken ct)
        {
            return await _agendaRepository.GetDailyAsync(date, ct);
        }

        public async Task<bool> ConfirmPreRegisterAsync(ConfirmPreRegisterDto dto, CancellationToken ct)
        {
            return await _agendaRepository.ConfirmPreRegisterAsync(dto, ct);
        }

        public async Task<int?> ConvertToWorkOrderAsync(int appointmentId, CancellationToken ct)
        {
            if (!int.TryParse(_currentUserService.UserId, out int responsibleUserId))
                return null;

            return await _agendaRepository.ConvertToWorkOrderAsync(appointmentId, responsibleUserId, ct);
        }

        public async Task<IEnumerable<AgendaDayConfigDto>> GetDayConfigsAsync(int? weeks, DateTime? start, CancellationToken ct)
        {
            return await _agendaRepository.GetDayConfigsAsync(weeks, start, ct);
        }

        public async Task<bool> UpdateDayConfigAsync(AgendaDayConfigDto dto, CancellationToken ct)
        {
            return await _agendaRepository.UpdateDayConfigAsync(dto, ct);
        }

        public async Task<bool> CancelAppointmentAsync(int appointmentId, CancellationToken ct)
        {
            return await _agendaRepository.CancelAppointmentAsync(appointmentId, ct);
        }

        public async Task<bool> RescheduleAsync(int appointmentId, DateTime newDate, CancellationToken ct)
        {
            return await _agendaRepository.RescheduleAsync(appointmentId, newDate, ct);
        }
    }
}
