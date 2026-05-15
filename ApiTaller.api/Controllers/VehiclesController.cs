using ApiTaller.Domain.Dtos.Vehicle;
using ApiTaller.Domain.Interfaces.Services.Vehicles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IVehicleService _vehicleService;

        public VehiclesController(ILogger<VehiclesController> logger, IVehicleService vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        [HttpGet("GetVehicles")]
        public async Task<IActionResult> GetVehicles(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _vehicleService.GetAllAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos");
            }
            return BadRequest();
        }

        [HttpGet("GetVehiclesActive")]
        public async Task<IActionResult> GetVehiclesActive(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _vehicleService.GetAllActiveAsync(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos activos");
            }
            return BadRequest();
        }

        [HttpGet("GetVehicle/{id}")]
        public async Task<IActionResult> GetVehicleById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _vehicleService.GetByIdAsync(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el vehículo con id {id}");
            }
            return BadRequest();
        }

        [HttpPost("CreateOrEditVehicle")]
        public async Task<IActionResult> CreateOrEditVehicle(GetVehicleDto value, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _vehicleService.CreateOrEditVehicle(value, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar el vehículo");
            }
            return BadRequest();
        }
    }
}
