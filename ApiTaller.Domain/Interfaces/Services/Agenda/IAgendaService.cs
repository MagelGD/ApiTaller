using ApiTaller.Domain.Dtos.Agenda;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Agenda
{
    public interface IAgendaService
    {
        Task<AgendaSettingsDto?> GetSettingsAsync(CancellationToken ct);
        Task<bool> UpdateSettingsAsync(AgendaSettingsDto dto, CancellationToken ct);
        Task<bool> AddBlockDateAsync(AgendaBlockDto dto, CancellationToken ct);
        Task<IEnumerable<string>> GetAvailableDatesAsync(CancellationToken ct);
        Task<bool> BookAsync(BookAppointmentDto dto, CancellationToken ct);
        Task<bool> PreRegisterAsync(PreRegisterAppointmentDto dto, CancellationToken ct);
        Task<bool> AdminBookAsync(AdminBookAppointmentDto dto, CancellationToken ct);
        Task<IEnumerable<AppointmentSummaryDto>> GetDailyAsync(DateTime date, CancellationToken ct);
        Task<bool> ConfirmPreRegisterAsync(ConfirmPreRegisterDto dto, CancellationToken ct);
        Task<int?> ConvertToWorkOrderAsync(int appointmentId, CancellationToken ct);
        Task<IEnumerable<AgendaDayConfigDto>> GetDayConfigsAsync(int? weeks, DateTime? start, CancellationToken ct);
        Task<bool> UpdateDayConfigAsync(AgendaDayConfigDto dto, CancellationToken ct);
        Task<bool> CancelAppointmentAsync(int appointmentId, CancellationToken ct);
        Task<bool> RescheduleAsync(int appointmentId, DateTime newDate, CancellationToken ct);
        
        // Excepciones de bloqueo
        Task<IEnumerable<AgendaBlockDto>> GetBlockedExceptionDatesAsync(CancellationToken ct);
        Task<bool> DeleteBlockedExceptionDateAsync(int id, CancellationToken ct);
    }
}
