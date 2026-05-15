using ApiTaller.Domain.Interfaces.Services.RoleActions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleActionsController : ControllerBase
    {
        private readonly ILogger<RoleActionsController> _logger;
        private readonly IRoleActionService _roleActionService;

        public RoleActionsController(ILogger<RoleActionsController> logger, IRoleActionService roleActionService)
        {
            _logger = logger;
            _roleActionService = roleActionService;
        }

        [HttpGet("GetRoleActions/{id}")]
        public async Task<IActionResult> GetRoleActions(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _roleActionService.GetActionsByRoleIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando las acciones del rol");
            }
            return BadRequest();
        }

        [HttpGet("PermissionRole/{id}")]
        public async Task<IActionResult> PermissionRole(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _roleActionService.GetActionsByRoleAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando las acciones del rol");
            }
            return BadRequest();
        }
    }
}
