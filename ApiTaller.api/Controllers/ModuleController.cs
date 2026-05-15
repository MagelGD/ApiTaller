using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Interfaces.Services.Module;
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
        public async Task<IActionResult> GetModules(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _moduleService.GetModules(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los módulos");
            }
            return BadRequest();
        }

        [HttpGet("GetModule/{id}")]
        public async Task<IActionResult> GetModuleById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _moduleService.GetModuleById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el módulo por id");
            }
            return BadRequest();
        }

        [HttpPost("SaveModule")]
        public async Task<IActionResult> SaveModule(GetModuleDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _moduleService.SaveOrEditModule(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el módulo");
            }
            return BadRequest();
        }
    }
}
