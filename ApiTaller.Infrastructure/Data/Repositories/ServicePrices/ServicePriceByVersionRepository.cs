using ApiTaller.Domain.Dtos.ServicePrices;
using ApiTaller.Domain.Interfaces.Repositories.ServicePrices;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.ServicePrices
{
    public class ServicePriceByVersionRepository : IServicePriceByVersionRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<ServicePriceByVersionRepository> _logger;

        public ServicePriceByVersionRepository(DataContext context, ICurrentUserService currentUserService, ILogger<ServicePriceByVersionRepository> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(ServicePriceByVersion create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.ServicePriceByVersion.AddAsync(create, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service price by version");
            }
            return false;
        }

        public async Task<IEnumerable<GetServicePriceByVersionDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.ServicePriceByVersion
                    .Where(sp => sp.IsActive)
                    .Select(sp => new GetServicePriceByVersionDto
                    {
                        Id = sp.Id,
                        ServiceCatalogId = sp.ServiceCatalogId,
                        BrandModelVersionId = sp.BrandModelVersionId,
                        Price = sp.Price,
                        EstimatedMinutes = sp.EstimatedMinutes,
                        IsActive = sp.IsActive,
                        CreatedAt = sp.CreatedAt,
                        UpdatedAt = sp.UpdatedAt
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service prices");
            }
            return new List<GetServicePriceByVersionDto>();
        }

        public async Task<IEnumerable<GetServicePriceByVersionDto>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.ServicePriceByVersion
                    .Select(sp => new GetServicePriceByVersionDto
                    {
                        Id = sp.Id,
                        ServiceCatalogId = sp.ServiceCatalogId,
                        BrandModelVersionId = sp.BrandModelVersionId,
                        Price = sp.Price,
                        EstimatedMinutes = sp.EstimatedMinutes,
                        IsActive = sp.IsActive,
                        CreatedAt = sp.CreatedAt,
                        UpdatedAt = sp.UpdatedAt
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all service prices");
            }
            return new List<GetServicePriceByVersionDto>();
        }

        public async Task<GetServicePriceByVersionDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                return await _context.ServicePriceByVersion
                    .Where(sp => sp.Id == id)
                    .Select(sp => new GetServicePriceByVersionDto
                    {
                        Id = sp.Id,
                        ServiceCatalogId = sp.ServiceCatalogId,
                        BrandModelVersionId = sp.BrandModelVersionId,
                        Price = sp.Price,
                        EstimatedMinutes = sp.EstimatedMinutes,
                        IsActive = sp.IsActive,
                        CreatedAt = sp.CreatedAt,
                        UpdatedAt = sp.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation) ?? new GetServicePriceByVersionDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service price {id}");
            }
            return null;
        }

        public async Task<bool> UpdateAsync(ServicePriceByVersion update, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _context.ServicePriceByVersion.Update(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating service price {update.Id}");
            }
            return false;
        }

        public async Task<GetServicePriceByVersionDto?> ValidateExist(int serviceCatalogId, int brandModelVersionId, CancellationToken cancellation)
        {
            try
            {
                return await _context.ServicePriceByVersion
                    .Where(sp => sp.ServiceCatalogId == serviceCatalogId && sp.BrandModelVersionId == brandModelVersionId)
                    .Select(sp => new GetServicePriceByVersionDto
                    {
                        Id = sp.Id,
                        ServiceCatalogId = sp.ServiceCatalogId,
                        BrandModelVersionId = sp.BrandModelVersionId,
                        Price = sp.Price,
                        EstimatedMinutes = sp.EstimatedMinutes,
                        IsActive = sp.IsActive,
                        CreatedAt = sp.CreatedAt,
                        UpdatedAt = sp.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of service price");
            }
            return null;
        }
    }
}
