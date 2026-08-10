using ApiTaller.Domain.Dtos.Brand;
using ApiTaller.Domain.Dtos.BrandModels;
using ApiTaller.Domain.Dtos.BrandModelVersion;
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
                if (!string.IsNullOrEmpty(create.Plate))
                {
                    create.Plate = create.Plate.ToUpper().Trim();
                }
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

        public async Task<IEnumerable<GetVehicleDto>> GetAllActiveAsync(string? vehicleType, CancellationToken cancellation)
        {
            IEnumerable<GetVehicleDto> result = new List<GetVehicleDto>();
            try
            {
                IQueryable<Domain.Models.Vehicle> query = _context.Vehicle.Where(v => v.IsActive);
                if (!string.IsNullOrWhiteSpace(vehicleType))
                {
                    query = query.Where(v => v.VehicleType == vehicleType);
                }

                result = await query
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
                        VehicleType = v.VehicleType,
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

        public async Task<IEnumerable<GetVehicleDto>> GetAllAsync(string? vehicleType, CancellationToken cancellation)
        {
            IEnumerable<GetVehicleDto> result = new List<GetVehicleDto>();
            try
            {
                IQueryable<Domain.Models.Vehicle> query = _context.Vehicle.Include(x => x.BrandNavigation).Include(x => x.ModelNavigation).Include(x => x.VersionNavigation).AsQueryable();
                if (!string.IsNullOrWhiteSpace(vehicleType))
                {
                    query = query.Where(v => v.VehicleType == vehicleType);
                }

                result = await query
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
                        VehicleType = v.VehicleType,
                        IsActive = v.IsActive,
                        CreatedAt = v.CreatedAt,
                        Brand = new GetBrandDto
                        {
                            Id = v.BrandNavigation.Id,
                            Name = v.BrandNavigation.Name,
                            VehicleType = v.BrandNavigation.VehicleType
                        },
                        Model = new GetBrandModelsDto
                        {
                            Id = v.ModelNavigation.Id,
                            Models = v.ModelNavigation.Models,
                            VehicleType = v.ModelNavigation.VehicleType
                        },
                        Reference = new GetBrandModelVersionDto
                        {
                            Id = v.VersionNavigation.Id,
                            Version = v.VersionNavigation.Version,
                            VehicleType = v.VersionNavigation.VehicleType
                        },
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
                        VehicleType = v.VehicleType,
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
                if (!string.IsNullOrEmpty(update.Plate))
                {
                    update.Plate = update.Plate.ToUpper().Trim();
                }
                Domain.Models.Vehicle? existingVehicle = await _context.Vehicle.FindAsync(new object[] { update.Id }, cancellation);
                if (existingVehicle == null) return false;

                if (int.TryParse(_currentUserService.UserId, out int userId))
                    update.ResponsibleUserId = userId;

                update.UpdatedAt = DateTime.Now;
                _context.Entry(existingVehicle).CurrentValues.SetValues(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el vehículo {Id}", update.Id);
                throw;
            }
        }

        public async Task<(bool HasActive, int WorkOrderId, string Status)?> GetActiveWorkOrderInfoAsync(int vehicleId, CancellationToken cancellation)
        {
            try
            {
                Domain.Models.WorkOrder? wo = await _context.WorkOrder
                    .Where(w => w.VehicleId == vehicleId && w.IsActive &&
                                (w.Status != "Entregado" || !_context.Sale.Any(s => s.WorkOrderId == w.Id && s.IsActive)))
                    .FirstOrDefaultAsync(cancellation);

                if (wo == null) return null;
                return (true, wo.Id, wo.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar órdenes activas del vehículo {Id}", vehicleId);
                return null;
            }
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
                        VehicleType = v.VehicleType,
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
