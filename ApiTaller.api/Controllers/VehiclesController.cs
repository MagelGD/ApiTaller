using ApiTaller.Domain.Dtos.Vehicle;
using ApiTaller.Domain.Interfaces.Services.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService, ILogger<VehiclesController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        [HttpGet("GetVehicles")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _vehicleService.GetAllAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos");
            }
            return BadRequest();
        }

        [HttpGet("GetVehiclesActive")]
        public async Task<IActionResult> GetActive(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _vehicleService.GetAllActiveAsync(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos activos");
            }
            return BadRequest();
        }

        [HttpGet("GetVehicle/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _vehicleService.GetByIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el vehículo con id {id}");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditVehicle")]
        public async Task<IActionResult> Post(GetVehicleDto value, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _vehicleService.CreateOrEditVehicle(value, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el vehículo");
            }
            return BadRequest();
        }
    }
}
