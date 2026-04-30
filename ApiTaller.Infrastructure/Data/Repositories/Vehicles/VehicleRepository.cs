using ApiTaller.Domain.Dtos.Vehicle;
using ApiTaller.Domain.Interfaces.Repositories.Vehicles;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Vehicles
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<VehicleRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public VehicleRepository(DataContext context, ILogger<VehicleRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<bool> CreateAsync(Vehicle create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.Vehicle.AddAsync(create, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating vehicle");
            }
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<IEnumerable<GetVehicleDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetVehicleDto> result = new List<GetVehicleDto>();
            try
            {
                result = await _context.Vehicle
                    .Where(v => v.IsActive)
                    .Select(v => new GetVehicleDto
                    {
                        Id = v.Id,
                        CustomerId = v.CustomerId,
                        Plate = v.Plate,
                        BrandId = v.BrandId,
                        ModelId = v.ModelId,
                        VersionId = v.VersionId,
                        Color = v.Color,
                        CylinderCapacity = v.CylinderCapacity,
                        IsActive = v.IsActive,
                        CreatedAt = v.CreatedAt,
                        UpdatedAt = v.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active vehicles");
            }
            return result;
        }

        public async Task<IEnumerable<GetVehicleDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetVehicleDto> result = new List<GetVehicleDto>();
            try
            {
                result = await _context.Vehicle
                    .Select(v => new GetVehicleDto
                    {
                        Id = v.Id,
                        CustomerId = v.CustomerId,
                        Plate = v.Plate,
                        BrandId = v.BrandId,
                        ModelId = v.ModelId,
                        VersionId = v.VersionId,
                        Color = v.Color,
                        CylinderCapacity = v.CylinderCapacity,
                        IsActive = v.IsActive,
                        CreatedAt = v.CreatedAt,
                        UpdatedAt = v.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all vehicles");
            }
            return result;
        }

        public async Task<GetVehicleDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetVehicleDto? result = null;
            try
            {
                result = await _context.Vehicle
                    .Where(v => v.Id == id)
                    .Select(v => new GetVehicleDto
                    {
                        Id = v.Id,
                        CustomerId = v.CustomerId,
                        Plate = v.Plate,
                        BrandId = v.BrandId,
                        ModelId = v.ModelId,
                        VersionId = v.VersionId,
                        Color = v.Color,
                        CylinderCapacity = v.CylinderCapacity,
                        IsActive = v.IsActive,
                        CreatedAt = v.CreatedAt,
                        UpdatedAt = v.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting vehicle by id {id}");
            }
            return result;
        }

        public async Task<bool> UpdateAsync(Vehicle update, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _context.Vehicle.Update(update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vehicle");
            }
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<GetVehicleDto?> ValidateExist(string plate, CancellationToken cancellation)
        {
            GetVehicleDto? result = null;
            try
            {
                result = await _context.Vehicle
                    .Where(v => v.Plate.ToLower() == plate.ToLower())
                    .Select(v => new GetVehicleDto
                    {
                        Id = v.Id,
                        CustomerId = v.CustomerId,
                        Plate = v.Plate,
                        BrandId = v.BrandId,
                        ModelId = v.ModelId,
                        VersionId = v.VersionId,
                        Color = v.Color,
                        CylinderCapacity = v.CylinderCapacity,
                        IsActive = v.IsActive,
                        CreatedAt = v.CreatedAt,
                        UpdatedAt = v.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating vehicle existence");
            }
            return result;
        }
    }
}
