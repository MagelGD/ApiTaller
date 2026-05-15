using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Interfaces.Services.UserRoles;
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
    public class UserRoleController : ControllerBase
    {
        private readonly ILogger<UserRoleController> _logger;
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(ILogger<UserRoleController> logger, IUserRoleService userRoleService)
        {
            _logger = logger;
            _userRoleService = userRoleService;
        }

        [HttpGet("GetUsersRoles")]
        public async Task<IActionResult> GetUsersRoles(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userRoleService.GetUserRoles(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los roles de los usuarios");
            }
            return BadRequest();
        }

        [HttpGet("GetUserRole/{id}")]
        public async Task<IActionResult> GetUserRoleById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userRoleService.GetUserRoleById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol del usuario por id");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditUserRole")]
        public async Task<IActionResult> SaveOrEditUserRole(GetUserRoleDto userRole, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userRoleService.SaveOrEditUserRole(userRole, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el rol del usuario");
            }
            return BadRequest();
        }
    }
}
