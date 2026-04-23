using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Interfaces.Services.UserRoleModules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserRoleModuleController : ControllerBase
    {

        private readonly ILogger<UserRoleModuleController> _logger;
        private readonly IUserRoleModuleService _userRoleModuleService;
        public UserRoleModuleController(ILogger<UserRoleModuleController> logger, IUserRoleModuleService userRoleModuleService)
        {
            _logger = logger;
            _userRoleModuleService = userRoleModuleService;
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

        // GET api/<UserRoleModuleController>/5
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

        // POST api/<UserRoleModuleController>
        [HttpPost("SaveOrEditUserRoleModule")]
        public async Task<IActionResult> SaveOrEditUserRoleModule(SaveUserRoleModuleDto saveUserRoleModule, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userRoleModuleService.SaveOrEditUserRoleModule(saveUserRoleModule, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el módulo de rol de usuario");
            }
            return BadRequest();
        }
    }
}
