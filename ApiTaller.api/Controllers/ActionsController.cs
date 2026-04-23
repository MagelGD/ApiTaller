using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Interfaces.Services.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly ILogger<ActionsController> _logger;
        private readonly IActionService _actionService;

        public ActionsController(ILogger<ActionsController> logger, IActionService actionService)
        {
            _logger = logger;
            _actionService = actionService;
        }
        // GET: api/<ActionsController>
        [HttpGet("GetActions")]
        public async Task<IActionResult> GetActions(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _actionService.GetActions(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las acciones");
            }
            return BadRequest();
        }

        [HttpGet("GetActionsActive")]
        public async Task<IActionResult> GetActionsActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _actionService.GetActionsActive(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las acciones");
            }
            return BadRequest();
        }

        // GET api/<ActionsController>/5
        [HttpGet("GetAction/{id}")]
        public async Task<IActionResult> GetActionById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _actionService.GetActionsById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la acción por id");
            }
            return BadRequest();
        }

        // POST api/<ActionsController>
        [HttpPost("SaveOrEditAction")]
        public async Task<IActionResult> SaveOrEditAction(GetActionsDto getAction, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _actionService.SaveOrEditActions(getAction, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar actions");
            }
            return BadRequest();
        }
    }
}
