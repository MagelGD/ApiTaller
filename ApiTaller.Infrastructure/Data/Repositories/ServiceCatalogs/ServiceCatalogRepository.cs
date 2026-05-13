using ApiTaller.Domain.Dtos.ServiceCatalogs;
using ApiTaller.Domain.Dtos.ServiceTypes;
using ApiTaller.Domain.Interfaces.Repositories.ServiceCatalogs;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.ServiceCatalogs
{
    public class ServiceCatalogRepository : IServiceCatalogRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<ServiceCatalogRepository> _logger;

        public ServiceCatalogRepository(DataContext context, ICurrentUserService currentUserService, ILogger<ServiceCatalogRepository> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(ServiceCatalog create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.ServiceCatalog.AddAsync(create, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service catalog");
            }
            return false;
        }

        public async Task<IEnumerable<GetServiceCatalogDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceCatalog
                    .Where(sc => sc.IsActive)
                    .Include(sc => sc.ServiceTypeIdNavigation)
                    .Select(sc => new GetServiceCatalogDto
                    {
                        Id = sc.Id,
                        ServiceTypeId = sc.ServiceTypeId,
                        Name = sc.Name,
                        Description = sc.Description,
                        DefaultPrice = sc.DefaultPrice,
                        DefaultMinutes = sc.DefaultMinutes,
                        TimeUnit = sc.TimeUnit,
                        IsActive = sc.IsActive,
                        CreatedAt = sc.CreatedAt,
                        UpdatedAt = sc.UpdatedAt,
                        GetServiceType = new GetServiceTypeDto
                        {
                            Id = sc.ServiceTypeIdNavigation.Id,
                            Name = sc.ServiceTypeIdNavigation.Name
                        }
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service catalogs");
            }
            return new List<GetServiceCatalogDto>();
        }

        public async Task<IEnumerable<GetServiceCatalogDto>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceCatalog
                    .Include(sc => sc.ServiceTypeIdNavigation)
                    .Select(sc => new GetServiceCatalogDto
                    {
                        Id = sc.Id,
                        ServiceTypeId = sc.ServiceTypeId,
                        Name = sc.Name,
                        Description = sc.Description,
                        DefaultPrice = sc.DefaultPrice,
                        DefaultMinutes = sc.DefaultMinutes,
                        TimeUnit = sc.TimeUnit,
                        IsActive = sc.IsActive,
                        CreatedAt = sc.CreatedAt,
                        UpdatedAt = sc.UpdatedAt,
                        GetServiceType = new GetServiceTypeDto
                        {
                            Id = sc.ServiceTypeIdNavigation.Id,
                            Name = sc.ServiceTypeIdNavigation.Name
                        }
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all service catalogs");
            }
            return new List<GetServiceCatalogDto>();
        }

        public async Task<GetServiceCatalogDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceCatalog
                    .Where(sc => sc.Id == id)
                    .Select(sc => new GetServiceCatalogDto
                    {
                        Id = sc.Id,
                        ServiceTypeId = sc.ServiceTypeId,
                        Name = sc.Name,
                        Description = sc.Description,
                        DefaultPrice = sc.DefaultPrice,
                        DefaultMinutes = sc.DefaultMinutes,
                        TimeUnit = sc.TimeUnit,
                        IsActive = sc.IsActive,
                        CreatedAt = sc.CreatedAt,
                        UpdatedAt = sc.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation) ?? new GetServiceCatalogDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service catalog {id}");
            }
            return null;
        }

        public async Task<bool> UpdateAsync(ServiceCatalog update, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _context.ServiceCatalog.Update(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating service catalog {update.Id}");
            }
            return false;
        }

        public async Task<GetServiceCatalogDto?> ValidateExist(string name, CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceCatalog
                    .Where(sc => sc.Name == name)
                    .Select(sc => new GetServiceCatalogDto
                    {
                        Id = sc.Id,
                        ServiceTypeId = sc.ServiceTypeId,
                        Name = sc.Name,
                        Description = sc.Description,
                        DefaultPrice = sc.DefaultPrice,
                        DefaultMinutes = sc.DefaultMinutes,
                        TimeUnit = sc.TimeUnit,
                        IsActive = sc.IsActive,
                        CreatedAt = sc.CreatedAt,
                        UpdatedAt = sc.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of service catalog {name}");
            }
            return null;
        }
    }
}
