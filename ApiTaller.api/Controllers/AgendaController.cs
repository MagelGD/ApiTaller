using ApiTaller.Domain.Dtos.Agenda;
using ApiTaller.Domain.Interfaces.Services.Agenda;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;

        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet("Settings")]
        public async Task<IActionResult> GetSettings(CancellationToken ct)
        {
            var result = await _agendaService.GetSettingsAsync(ct);
            if (result == null) return NoContent();
            return Ok(result);
        }

        [HttpPut("Settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] AgendaSettingsDto dto, CancellationToken ct)
        {
            var result = await _agendaService.UpdateSettingsAsync(dto, ct);
            if (!result) return BadRequest(new { Message = "Error al actualizar la configuración." });
            return Ok(new { Message = "Configuración actualizada con éxito." });
        }

        [HttpPost("BlockDate")]
        public async Task<IActionResult> BlockDate([FromBody] AgendaBlockDto dto, CancellationToken ct)
        {
            var result = await _agendaService.AddBlockDateAsync(dto, ct);
            if (!result) return BadRequest(new { Message = "La fecha ya se encuentra bloqueada." });
            return Ok(new { Message = "Día bloqueado con éxito." });
        }

        [AllowAnonymous]
        [HttpGet("AvailableDates")]
        public async Task<IActionResult> GetAvailableDates(CancellationToken ct)
        {
            var result = await _agendaService.GetAvailableDatesAsync(ct);
            return Ok(result);
        }

        [HttpPost("Book")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentDto dto, CancellationToken ct)
        {
            var result = await _agendaService.BookAsync(dto, ct);
            if (!result) return BadRequest(new { Message = "Error al agendar la cita. Verifique sus datos o la disponibilidad." });
            return Ok(new { Message = "Cita agendada con éxito." });
        }

        [AllowAnonymous]
        [HttpPost("PreRegister")]
        public async Task<IActionResult> PreRegister([FromBody] PreRegisterAppointmentDto dto, CancellationToken ct)
        {
            var result = await _agendaService.PreRegisterAsync(dto, ct);
            if (!result) return BadRequest(new { Message = "Error al registrar la solicitud." });
            return Ok(new { Message = "Solicitud de cita enviada. Nos pondremos en contacto pronto." });
        }

        [HttpPost("AdminBook")]
        public async Task<IActionResult> AdminBook([FromBody] AdminBookAppointmentDto dto, CancellationToken ct)
        {
            var result = await _agendaService.AdminBookAsync(dto, ct);
            if (!result) return BadRequest(new { Message = "No hay cupos disponibles para esta fecha. Use 'Forzar Agendamiento' si es necesario." });
            return Ok(new { Message = "Cita agendada con éxito." });
        }

        [HttpGet("Daily")]
        public async Task<IActionResult> GetDaily([FromQuery] DateTime date, CancellationToken ct)
        {
            var result = await _agendaService.GetDailyAsync(date, ct);
            return Ok(result);
        }

        [HttpPost("Confirm/{id}")]
        public async Task<IActionResult> ConfirmPreRegister(int id, [FromBody] ConfirmPreRegisterDto dto, CancellationToken ct)
        {
            if (id != dto.AppointmentId) return BadRequest();

            var result = await _agendaService.ConfirmPreRegisterAsync(dto, ct);
            if (!result) return BadRequest(new { Message = "Error al confirmar la cita o la cita no está pendiente." });
            return Ok(new { Message = "Cita confirmada con éxito." });
        }

        [HttpPost("ConvertToWorkOrder/{id}")]
        public async Task<IActionResult> ConvertToWorkOrder(int id, CancellationToken ct)
        {
            var workOrderId = await _agendaService.ConvertToWorkOrderAsync(id, ct);
            if (workOrderId == null) return BadRequest(new { Message = "No se pudo convertir la cita. Verifique que la cita exista y tenga un vehículo asociado." });
            return Ok(new { Message = "Orden de trabajo creada con éxito.", WorkOrderId = workOrderId });
        }

        [HttpGet("DayConfigs")]
        public async Task<IActionResult> GetDayConfigs([FromQuery] int? weeks, [FromQuery] DateTime? start, CancellationToken ct)
        {
            return Ok(await _agendaService.GetDayConfigsAsync(weeks, start, ct));
        }

        [HttpPut("DayConfig")]
        public async Task<IActionResult> UpdateDayConfig([FromBody] AgendaDayConfigDto dto, CancellationToken ct)
        {
            var result = await _agendaService.UpdateDayConfigAsync(dto, ct);
            if (!result) return BadRequest();
            return Ok(new { Message = "Configuración del día actualizada." });
        }

        [HttpPost("Cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id, CancellationToken ct)
        {
            var result = await _agendaService.CancelAppointmentAsync(id, ct);
            if (!result) return BadRequest(new { Message = "No se pudo cancelar la cita." });
            return Ok(new { Message = "Cita cancelada con éxito." });
        }

        [HttpPost("Reschedule/{id}")]
        public async Task<IActionResult> Reschedule(int id, [FromQuery] DateTime date, CancellationToken ct)
        {
            var result = await _agendaService.RescheduleAsync(id, date, ct);
            if (!result) return BadRequest(new { Message = "No se pudo reprogramar la cita." });
            return Ok(new { Message = "Cita reprogramada con éxito." });
        }
    }
}
