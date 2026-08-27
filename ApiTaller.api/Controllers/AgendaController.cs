using ApiTaller.Domain.Dtos.Agenda;
using ApiTaller.Domain.Interfaces.Services.Agenda;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

using ApiTaller.Domain.Constants;
using ApiTaller.api.Filters;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [RequireTenantModule(ModuleConstants.Agenda)]
    public class AgendaController : ControllerBase
    {
        private readonly ILogger<AgendaController> _logger;
        private readonly IAgendaService _agendaService;

        public AgendaController(ILogger<AgendaController> logger, IAgendaService agendaService)
        {
            _logger = logger;
            _agendaService = agendaService;
        }

        [HttpGet("Settings")]
        public async Task<IActionResult> GetSettings(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.GetSettingsAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración de la agenda");
            }
            return BadRequest();
        }

        [HttpPut("Settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] AgendaSettingsDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.UpdateSettingsAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la configuración de la agenda");
            }
            return BadRequest();
        }

        [HttpPost("BlockDate")]
        public async Task<IActionResult> BlockDate([FromBody] AgendaBlockDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.AddBlockDateAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al bloquear fecha");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("AvailableDates")]
        public async Task<IActionResult> GetAvailableDates(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.GetAvailableDatesAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fechas disponibles");
            }
            return BadRequest();
        }

        [HttpPost("Book")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.BookAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agendar cita");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("PreRegister")]
        public async Task<IActionResult> PreRegister([FromBody] PreRegisterAppointmentDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.PreRegisterAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al pre-registrar cita");
            }
            return BadRequest();
        }

        [HttpPost("AdminBook")]
        public async Task<IActionResult> AdminBook([FromBody] AdminBookAppointmentDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.AdminBookAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agendar cita (Admin)");
            }
            return BadRequest();
        }

        [HttpGet("Daily")]
        public async Task<IActionResult> GetDaily([FromQuery] DateTime date, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.GetDailyAsync(date, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener agenda diaria");
            }
            return BadRequest();
        }

        [HttpPost("Confirm/{id}")]
        public async Task<IActionResult> ConfirmPreRegister(int id, [FromBody] ConfirmPreRegisterDto dto, CancellationToken cancellation)
        {
            try
            {
                if (id != dto.AppointmentId) return BadRequest();
                return Ok(await _agendaService.ConfirmPreRegisterAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al confirmar cita");
            }
            return BadRequest();
        }

        [HttpPost("ConvertToWorkOrder/{id}")]
        public async Task<IActionResult> ConvertToWorkOrder(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.ConvertToWorkOrderAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al convertir cita a OT");
            }
            return BadRequest();
        }

        [HttpGet("DayConfigs")]
        public async Task<IActionResult> GetDayConfigs([FromQuery] int? weeks, [FromQuery] DateTime? start, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.GetDayConfigsAsync(weeks, start, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener configuración de días");
            }
            return BadRequest();
        }

        [HttpPut("DayConfig")]
        public async Task<IActionResult> UpdateDayConfig([FromBody] AgendaDayConfigDto dto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.UpdateDayConfigAsync(dto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar configuración de día");
            }
            return BadRequest();
        }

        [HttpPost("Cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.CancelAppointmentAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cancelar cita");
            }
            return BadRequest();
        }

        [HttpPost("Reschedule/{id}")]
        public async Task<IActionResult> Reschedule(int id, [FromQuery] DateTime date, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.RescheduleAsync(id, date, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reprogramar cita");
            }
            return BadRequest();
        }

        [HttpGet("BlockedDates")]
        public async Task<IActionResult> GetBlockedDates(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.GetBlockedExceptionDatesAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener fechas excepcionales bloqueadas");
            }
            return BadRequest();
        }

        [HttpDelete("BlockDate/{id}")]
        public async Task<IActionResult> DeleteBlockedExceptionDate(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _agendaService.DeleteBlockedExceptionDateAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desbloquear fecha excepcional");
            }
            return BadRequest();
        }
    }
}
