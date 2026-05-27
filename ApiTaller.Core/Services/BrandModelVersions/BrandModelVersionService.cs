using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Services.BrandModelVersion;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.BrandModelVersions
{
    public class BrandModelVersionService : IBrandModelVersionService
    {
        private readonly IBrandModelVersionRepository _brandModelVersionRepository;
        private readonly ILogger<BrandModelVersionService> _logger;

        public BrandModelVersionService(IBrandModelVersionRepository brandModelVersionRepository, ILogger<BrandModelVersionService> logger)
        {
            _brandModelVersionRepository = brandModelVersionRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _brandModelVersionRepository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca activas.");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetAllActiveAsync(string? vehicleType, CancellationToken cancellation)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _brandModelVersionRepository.GetAllActiveAsync(vehicleType, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca activas filtradas por vehicleType.");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _brandModelVersionRepository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca.");
            }
            return result;
        }

        public async Task<GetBrandModelVersionDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _brandModelVersionRepository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la versión de modelo de marca con ID {id}.");
            }
            return result;
        }
        public async Task<GetBrandModelVersionDto> CreateOrEditAsync(GetBrandModelVersionDto dto, CancellationToken cancellation)
        {
            GetBrandModelVersionDto result = new();
            try
            {
                BrandModelVersion saveData = new()
                {
                    Id = dto.Id,
                    Version = dto.Version,
                    BrandId = dto.brandDto.Id,
                    ModelId = dto.BrandModelsDto.Id,
                    VehicleType = dto.VehicleType,
                    IsActive = dto.IsActive,
                    CreatedAt = dto.CreatedAt ?? DateTime.Now
                };
                bool isExit = await ValidateExist(dto, cancellation);
                if (saveData.Id == 0 && !isExit)
                {
                    await _brandModelVersionRepository.CreateAsync(saveData, cancellation);
                }
                else if (saveData.Id != 0)
                {
                    await _brandModelVersionRepository.UpdateAsync(saveData, cancellation);
                }
                result = await _brandModelVersionRepository.ValidateExist(dto, cancellation) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar la versión de modelo de marca.");
            }
            return result;
        }
        private async Task<bool> ValidateExist(GetBrandModelVersionDto dto, CancellationToken cancellation)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _brandModelVersionRepository.ValidateExist(dto, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar la existencia de la versión de modelo de marca.");
            }
            return result != null;
        }
    }
}
