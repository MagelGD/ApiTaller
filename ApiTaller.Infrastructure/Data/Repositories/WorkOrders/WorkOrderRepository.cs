using ApiTaller.Domain.Dtos.WorkOrder;
using ApiTaller.Domain.Interfaces.Repositories.WorkOrders;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.WorkOrders
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<WorkOrderRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public WorkOrderRepository(DataContext context, ILogger<WorkOrderRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<bool> CreateAsync(WorkOrder create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                create.IsActive = true;
                
                await _context.WorkOrder.AddAsync(create, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating work order");
                return false;
            }
        }

        public async Task<IEnumerable<WorkOrderDto>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.WorkOrder
                    .Include(w => w.CustomerNavigation)
                    .Include(w => w.VehicleNavigation)
                    .Select(w => new WorkOrderDto
                    {
                        Id = w.Id,
                        VehicleId = w.VehicleId,
                        VehiclePlate = w.VehicleNavigation.Plate,
                        CustomerId = w.CustomerId,
                        CustomerName = w.CustomerNavigation.FirstName + " " + w.CustomerNavigation.LastName,
                        EntryDate = w.EntryDate,
                        EstimatedDeliveryDate = w.EstimatedDeliveryDate,
                        Mileage = w.Mileage,
                        FuelLevel = w.FuelLevel,
                        Observations = w.Observations,
                        Status = w.Status,
                        IsActive = w.IsActive,
                        CreatedAt = w.CreatedAt,
                        UpdatedAt = w.UpdatedAt
                    })
                    .OrderByDescending(w => w.CreatedAt)
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all work orders");
                return new List<WorkOrderDto>();
            }
        }

        public async Task<WorkOrderDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            try
            {
                return await _context.WorkOrder
                    .Include(w => w.CustomerNavigation)
                    .Include(w => w.VehicleNavigation)
                    .Include(w => w.Evidences)
                    .Include(w => w.Parts).ThenInclude(p => p.ProductNavigation)
                    .Include(w => w.Services).ThenInclude(s => s.MechanicNavigation)
                    .Where(w => w.Id == id)
                    .Select(w => new WorkOrderDto
                    {
                        Id = w.Id,
                        VehicleId = w.VehicleId,
                        VehiclePlate = w.VehicleNavigation.Plate,
                        CustomerId = w.CustomerId,
                        CustomerName = w.CustomerNavigation.FirstName + " " + w.CustomerNavigation.LastName,
                        EntryDate = w.EntryDate,
                        EstimatedDeliveryDate = w.EstimatedDeliveryDate,
                        Mileage = w.Mileage,
                        FuelLevel = w.FuelLevel,
                        Observations = w.Observations,
                        Status = w.Status,
                        IsActive = w.IsActive,
                        CreatedAt = w.CreatedAt,
                        UpdatedAt = w.UpdatedAt,
                        Evidences = w.Evidences.Select(e => new WorkOrderEvidenceDto
                        {
                            Id = e.Id,
                            WorkOrderId = e.WorkOrderId,
                            PhotoUrl = e.PhotoUrl,
                            EvidenceType = e.EvidenceType,
                            Description = e.Description,
                            IsActive = e.IsActive
                        }).ToList(),
                        Parts = w.Parts.Select(p => new WorkOrderPartDto
                        {
                            Id = p.Id,
                            WorkOrderId = p.WorkOrderId,
                            ProductId = p.ProductId,
                            ProductName = p.ProductNavigation.ProductName,
                            PartName = p.PartName,
                            Quantity = p.Quantity,
                            UnitPrice = p.UnitPrice,
                            IsProvidedByCustomer = p.IsProvidedByCustomer,
                            WarrantyEndDate = p.WarrantyEndDate,
                            IsActive = p.IsActive
                        }).ToList(),
                        Services = w.Services.Select(s => new WorkOrderServiceDto
                        {
                            Id = s.Id,
                            WorkOrderId = s.WorkOrderId,
                            Description = s.Description,
                            MechanicId = s.MechanicId,
                            MechanicName = s.MechanicNavigation.FullName,
                            Price = s.Price,
                            WarrantyEndDate = s.WarrantyEndDate,
                            IsActive = s.IsActive
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting work order by id {id}");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(WorkOrder update, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                
                _context.WorkOrder.Update(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating work order");
                return false;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id, string status, CancellationToken cancellation)
        {
            try
            {
                var workOrder = await _context.WorkOrder.FindAsync(new object[] { id }, cancellation);
                if (workOrder == null) return false;

                workOrder.Status = status;
                workOrder.UpdatedAt = DateTime.Now;
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    workOrder.ResponsibleUserId = userId;
                }

                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing work order status");
                return false;
            }
        }
    }
}
