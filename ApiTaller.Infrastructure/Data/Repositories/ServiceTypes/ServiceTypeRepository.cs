using ApiTaller.Domain.Dtos.ServiceTypes;
using ApiTaller.Domain.Interfaces.Repositories.ServiceTypes;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.ServiceTypes
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<ServiceTypeRepository> _logger;

        public ServiceTypeRepository(DataContext context, ICurrentUserService currentUserService, ILogger<ServiceTypeRepository> logger)
        {
            _context = context;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(ServiceType create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.ServiceType.AddAsync(create, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating service type");
            }
            return false;
        }

        public async Task<IEnumerable<GetServiceTypeDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceType
                    .Where(st => st.IsActive)
                    .Select(st => new GetServiceTypeDto
                    {
                        Id = st.Id,
                        Name = st.Name,
                        IsActive = st.IsActive,
                        CreatedAt = st.CreatedAt,
                        UpdatedAt = st.UpdatedAt
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving active service types");
            }
            return new List<GetServiceTypeDto>();
        }

        public async Task<IEnumerable<GetServiceTypeDto>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceType
                    .Select(st => new GetServiceTypeDto
                    {
                        Id = st.Id,
                        Name = st.Name,
                        IsActive = st.IsActive,
                        CreatedAt = st.CreatedAt,
                        UpdatedAt = st.UpdatedAt
                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all service types");
            }
            return new List<GetServiceTypeDto>();
        }

        public async Task<GetServiceTypeDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceType
                    .Where(st => st.Id == id)
                    .Select(st => new GetServiceTypeDto
                    {
                        Id = st.Id,
                        Name = st.Name,
                        IsActive = st.IsActive,
                        CreatedAt = st.CreatedAt,
                        UpdatedAt = st.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation) ?? new GetServiceTypeDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving service type {id}");
            }
            return null;
        }

        public async Task<bool> UpdateAsync(ServiceType update, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _context.ServiceType.Update(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating service type {update.Id}");
            }
            return false;
        }

        public async Task<GetServiceTypeDto?> ValidateExist(string name, CancellationToken cancellation)
        {
            try
            {
                return await _context.ServiceType
                    .Where(st => st.Name == name)
                    .Select(st => new GetServiceTypeDto
                    {
                        Id = st.Id,
                        Name = st.Name,
                        IsActive = st.IsActive,
                        CreatedAt = st.CreatedAt,
                        UpdatedAt = st.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of service type {name}");
            }
            return null;
        }
    }
}
