using ApiTaller.api.Hubs;
using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Interfaces.Services.UserRoleModules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleModuleController : ControllerBase
    {
        private readonly ILogger<UserRoleModuleController> _logger;
        private readonly IUserRoleModuleService _userRoleModuleService;
        private readonly IHubContext<PermissionsHub> _hubContext;

        public UserRoleModuleController(ILogger<UserRoleModuleController> logger, IUserRoleModuleService userRoleModuleService, IHubContext<PermissionsHub> hubContext)
        {
            _logger = logger;
            _userRoleModuleService = userRoleModuleService;
            _hubContext = hubContext;
        }

        [HttpGet("GetUserRoleModule")]
        public async Task<IActionResult> GetUserRoleModule(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userRoleModuleService.GetUserRoleModules(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los módulos de rol de usuario");
            }
            return BadRequest();
        }

        [HttpGet("GetUserRoleModule/{id}")]
        public async Task<IActionResult> GetUserRoleModuleById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userRoleModuleService.GetUserRoleModuleById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el módulo de rol de usuario por id");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditUserRoleModule")]
        public async Task<IActionResult> SaveOrEditUserRoleModule(SaveUserRoleModuleDto saveUserRoleModule, CancellationToken cancellation)
        {
            try
            {
                var result = await _userRoleModuleService.SaveOrEditUserRoleModule(saveUserRoleModule, cancellation);
                await _hubContext.Clients.All.SendAsync("PermissionsChanged", result.Role.IdUserRol);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el módulo de rol de usuario");
            }
            return BadRequest();
        }
    }
}
