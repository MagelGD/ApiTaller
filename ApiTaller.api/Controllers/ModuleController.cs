using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Interfaces.Services.Module;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [Authorize]
    public class ModuleController : ControllerBase
    {
        private readonly ILogger<ModuleController> _logger;
        private readonly IModuleService _moduleService;
        public ModuleController(ILogger<ModuleController> logger, IModuleService moduleService)
        {
            _logger = logger;
            _moduleService = moduleService;
        }
        [HttpGet("GetModules")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _moduleService.GetModules(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los módulos");
            }
            return BadRequest();
        }
        [HttpGet("GetModule{id}")]
        public async Task<IActionResult> GetId(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _moduleService.GetModuleById(id, cancellationToken));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al obtener el módulo por id");
            }
            return BadRequest();
        }
        [HttpPost("SaveModule")]
        public async Task<IActionResult> Post(GetModuleDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _moduleService.SaveOrEditModule(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el módulo");
            }
            return BadRequest();
        }
    }
}
