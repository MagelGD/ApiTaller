using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Interfaces.Repositories.BrandModels;
using ApiTaller.Domain.Interfaces.Services.BrandModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.BrandModels
{
    public class BrandModelService : IBrandModelsService
    {
        private readonly IBrandModelsRepository _brandRepository;
        private readonly ILogger<BrandModelService> _logger;

        public BrandModelService(IBrandModelsRepository brandModelsRepository, ILogger<BrandModelService> logger)
        {
            _brandRepository = brandModelsRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsActiveAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelsDto> result = [];
            try
            {
                result = await _brandRepository.GetAllBrandModelsActiveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<IEnumerable<GetBrandModelsDto>> GetAllBrandModelsAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandModelsDto> result = [];
            try
            {
                result = await _brandRepository.GetAllBrandModelsAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<GetBrandModelsDto?> GetBrandModelByIdAsync(int id, CancellationToken cancellationToken)
        {
            GetBrandModelsDto? result = null;
            try
            {
                result = await _brandRepository.GetBrandModelByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        public async Task<GetBrandModelsDto> CreateOrEditBrandModel(GetBrandModelsDto brandModelDto, CancellationToken cancellationToken)
        {
            GetBrandModelsDto result = new();
            try
            {
                Domain.Models.BrandModels saveData = new()
                {
                    Id = brandModelDto.Id,
                    Models = brandModelDto.Models,
                    IsActive = brandModelDto.IsActive,
                    CreatedAt = brandModelDto.CreatedAt
                };
                bool isExist = await ValidateExist(brandModelDto, cancellationToken);
                if (saveData.Id == 0 && !isExist)
                {
                    await _brandRepository.CreateBrandModelAsync(saveData, cancellationToken);
                }
                else if (saveData.Id != 0)
                {
                    await _brandRepository.UpdateBrandModelAsync(saveData, cancellationToken);
                }
                result = await _brandRepository.ValidateExist(brandModelDto, cancellationToken) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result;
        }

        private async Task<bool> ValidateExist(GetBrandModelsDto? data, CancellationToken cancellationToken)
        {
            GetBrandModelsDto? result = null;
            try
            {
                result = await _brandRepository.ValidateExist(data, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return result != null;
        }
    }
}
