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


        public async Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionActiveAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _brandModelVersionRepository.GetBrandModelVersionActiveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca activas.");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelVersionDto> result = [];
            try
            {
                result = await _brandModelVersionRepository.GetBrandModelVersionAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las versiones de modelos de marca.");
            }
            return result;
        }

        public async Task<GetBrandModelVersionDto?> GetBrandModelVersionByIdAsync(int id, CancellationToken cancellationToken)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _brandModelVersionRepository.GetBrandModelVersionByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la versión de modelo de marca con ID {id}.");
            }
            return result;
        }
        public async Task<GetBrandModelVersionDto> CreateOrEditBrandModelVersionAsync(GetBrandModelVersionDto getBrandModelVersionDto, CancellationToken cancellationToken)
        {
            GetBrandModelVersionDto result = new();
            try
            {
                BrandModelVersion saveData = new()
                {
                    Id = getBrandModelVersionDto.Id,
                    Version = getBrandModelVersionDto.Version,
                    BrandId = getBrandModelVersionDto.brandDto.Id,
                    ModelId = getBrandModelVersionDto.BrandModelsDto.Id,
                    IsActive = getBrandModelVersionDto.IsActive,
                    CreatedAt = getBrandModelVersionDto.CreatedAt
                };
                bool isExit = await ValidateExist(getBrandModelVersionDto, cancellationToken);
                if (saveData.Id == 0 && !isExit)
                {
                    await _brandModelVersionRepository.CreateBrandModelVersionAsync(saveData, cancellationToken);
                }
                else if (saveData.Id != 0)
                {
                    await _brandModelVersionRepository.UpdateBrandModelVersionAsync(saveData, cancellationToken);
                }
                result = await _brandModelVersionRepository.ValidateExist(getBrandModelVersionDto, cancellationToken) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear o editar la versión de modelo de marca.");
            }
            return result;
        }
        private async Task<bool> ValidateExist(GetBrandModelVersionDto getBrandModelVersionDto, CancellationToken cancellationToken)
        {
            GetBrandModelVersionDto? result = null;
            try
            {
                result = await _brandModelVersionRepository.ValidateExist(getBrandModelVersionDto, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar la existencia de la versión de modelo de marca.");
            }
            return result != null;
        }
    }
}
