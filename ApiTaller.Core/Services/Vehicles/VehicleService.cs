using ApiTaller.Domain.Dtos.Vehicle;
using ApiTaller.Domain.Interfaces.Repositories.Vehicles;
using ApiTaller.Domain.Interfaces.Services.Vehicles;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(IVehicleRepository vehicleRepository, ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public async Task<GetVehicleDto> CreateOrEditVehicle(GetVehicleDto vehicle, CancellationToken cancellationToken)
        {
            GetVehicleDto result = new();
            try
            {
                Vehicle saveData = new()
                {
                    Id = vehicle.Id,
                    CustomerId = vehicle.CustomerId,
                    Plate = vehicle.Plate,
                    BrandId = vehicle.BrandId,
                    ModelId = vehicle.ModelId,
                    VersionId = vehicle.VersionId,
                    Color = vehicle.Color,
                    CylinderCapacity = vehicle.CylinderCapacity,
                    IsActive = vehicle.IsActive,
                    CreatedAt = vehicle.CreatedAt ?? DateTime.Now
                };
                
                bool isExist = await ValidateExist(vehicle.Plate, cancellationToken);
                
                if (saveData.Id == 0 && !isExist)
                {
                    await _vehicleRepository.CreateAsync(saveData, cancellationToken);
                }
                else if (saveData.Id != 0)
                {
                    await _vehicleRepository.UpdateAsync(saveData, cancellationToken);
                }
                
                result = await _vehicleRepository.ValidateExist(vehicle.Plate, cancellationToken) ?? new GetVehicleDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear o editar el vehículo con placa {vehicle.Plate}");
            }
            return result;
        }

        public async Task<IEnumerable<GetVehicleDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetVehicleDto> result = [];
            try
            {
                result = await _vehicleRepository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los vehículos activos");
            }
            return result;
        }

        public async Task<IEnumerable<GetVehicleDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetVehicleDto> result = [];
            try
            {
                result = await _vehicleRepository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los vehículos");
            }
            return result;
        }

        public async Task<GetVehicleDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetVehicleDto? result = null;
            try
            {
                result = await _vehicleRepository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el vehículo con ID {id}");
            }
            return result;
        }

        private async Task<bool> ValidateExist(string plate, CancellationToken cancellation)
        {
            bool result = false;
            try
            {
                var existingVehicle = await _vehicleRepository.ValidateExist(plate, cancellation);
                result = existingVehicle != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar la existencia del vehículo con placa {plate}");
            }
            return result;
        }
    }
}
