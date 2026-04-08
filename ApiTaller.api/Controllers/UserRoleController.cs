using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Interfaces.Services.UserRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;
        private readonly ILogger<UserRoleController> _logger;
        public UserRoleController(IUserRoleService userRoleService, ILogger<UserRoleController> logger)
        {
            _userRoleService = userRoleService;
            _logger = logger;
        }

        [HttpGet("GetUsersRoles")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userRoleService.GetUserRoles(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los roles de los usuarios");
            }
            return BadRequest();
        }

        [HttpGet("GetUserRoleId/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userRoleService.GetUserRoleById(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol del usuario por id");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditUserRole")]
        public async Task<IActionResult> SaveOrEdit(GetUserRole userRole, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userRoleService.SaveOrEditUserRole(userRole, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el rol del usuario");
            }
            return BadRequest();
        }
    }
}
