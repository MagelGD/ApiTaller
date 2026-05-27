using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Interfaces.Repositories.Brands;
using ApiTaller.Domain.Interfaces.Services.Brands;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Brands
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _BrandRepository;
        private readonly ILogger<BrandService> _logger;

        public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger)
        {
            _BrandRepository = brandRepository;
            _logger = logger;
        }


        public async Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandDto> activeBrands = [];
            try
            {
                activeBrands = await _BrandRepository.GetAllBrandsActiveAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active brands");
            }
            return activeBrands;
        }

        public async Task<IEnumerable<GetBrandDto>> GetAllBrandsActiveAsync(string? vehicleType, CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandDto> activeBrands = [];
            try
            {
                activeBrands = await _BrandRepository.GetAllBrandsActiveAsync(vehicleType, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active brands filtered by vehicleType");
            }
            return activeBrands;
        }

        public async Task<IEnumerable<GetBrandDto>> GetAllBrandsAsync(CancellationToken cancellationToken)
        {
            IEnumerable<GetBrandDto> allBrands = [];
            try
            {
                allBrands = await _BrandRepository.GetAllBrandsAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all brands");
            }
            return allBrands;
        }

        public async Task<GetBrandDto?> GetBrandByIdAsync(int id, CancellationToken cancellationToken)
        {
            GetBrandDto? brand = null;
            try
            {
                brand = await _BrandRepository.GetBrandByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving brand with ID {BrandId}", id);
            }
            return brand;
        }
        public async Task<GetBrandDto> CreateOrEditBrand(GetBrandDto brandDto, CancellationToken cancellationToken)
        {
            GetBrandDto brand = new();
            try
            {
                Brand saveData = new()
                {
                    Id = brandDto.Id,
                    Name = brandDto.Name,
                    VehicleType = brandDto.VehicleType,
                    IsActive = brandDto.IsActive,
                    CreatedAt = brandDto.CreatedAt
                };
                bool isExist = await ValidateBrandExistence(brandDto, cancellationToken);
                if (saveData.Id == 0 && !isExist)
                {
                    await _BrandRepository.CreateBrandAsync(saveData, cancellationToken);
                }
                else if (saveData.Id != 0 && isExist)
                {
                    await _BrandRepository.UpdateBrandAsync(saveData, cancellationToken);
                }
                brand = await _BrandRepository.GetBrandByIdAsync(saveData.Id, cancellationToken) ?? new GetBrandDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or editing brand with name {BrandName}", brandDto.Name);
            }
            return brand;
        }
        private async Task<bool> ValidateBrandExistence(GetBrandDto? brandDto, CancellationToken cancellationToken)
        {
            bool exists = false;
            try
            {
                if (brandDto != null)
                {
                    GetBrandDto? existingBrand = await _BrandRepository.ValidateExist(brandDto, cancellationToken);
                    exists = existingBrand != null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating existence of brand with name {BrandName}", brandDto?.Name);
            }
            return exists;
        }
    }
}
