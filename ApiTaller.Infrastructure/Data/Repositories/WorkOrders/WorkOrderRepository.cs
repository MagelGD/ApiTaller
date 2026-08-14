using ApiTaller.Domain.Dtos;
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
                int.TryParse(_currentUserService.UserId, out int userId);

                create.CreatedAt = DateTime.Now;
                create.IsActive = true;
                if (userId != 0)
                {
                    create.ResponsibleUserId = userId;
                    foreach (WorkOrderPart p in create.Parts) p.ResponsibleUserId = userId;
                    foreach (WorkOrderService s in create.Services) s.ResponsibleUserId = userId;
                    foreach (WorkOrderEvidence e in create.Evidences) e.ResponsibleUserId = userId;
                }

                await _context.WorkOrder.AddAsync(create, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la orden de trabajo");
                throw;
            }
        }

        public async Task<IEnumerable<WorkOrderDto>> GetAllAsync(string? vehicleType, CancellationToken cancellation)
        {
            try
            {
                IQueryable<WorkOrder> query = _context.WorkOrder.AsQueryable();
                if (!string.IsNullOrWhiteSpace(vehicleType))
                {
                    query = query.Where(w => w.VehicleNavigation.VehicleType == vehicleType);
                }

                return await query
                    .Include(w => w.CustomerNavigation)
                    .Include(w => w.VehicleNavigation)
                    .Select(w => new WorkOrderDto
                    {
                        Id = w.Id,
                        VehicleId = w.VehicleId,
                        VehiclePlate = w.VehicleNavigation != null ? w.VehicleNavigation.Plate : null,
                        VehicleBrand = w.VehicleNavigation != null && w.VehicleNavigation.BrandNavigation != null ? w.VehicleNavigation.BrandNavigation.Name : null,
                        VehicleVersion = w.VehicleNavigation != null && w.VehicleNavigation.ModelNavigation != null ? w.VehicleNavigation.ModelNavigation.Models : null,
                        VehicleType = w.VehicleNavigation != null ? w.VehicleNavigation.VehicleType : "moto",
                        VehicleMotorization = w.VehicleNavigation != null ? w.VehicleNavigation.CylinderCapacity : null,
                        CustomerId = w.CustomerId,
                        CustomerName = w.CustomerNavigation != null ? w.CustomerNavigation.FirstName + " " + w.CustomerNavigation.LastName : null,
                        EntryDate = w.EntryDate,
                        EstimatedDeliveryDate = w.EstimatedDeliveryDate,
                        Mileage = w.Mileage,
                        FuelLevel = w.FuelLevel,
                        Observations = w.Observations,
                        Status = w.Status,
                        IsActive = w.IsActive,
                        IsBilled = _context.Sale.Any(s => s.WorkOrderId == w.Id && s.IsActive),
                        DownPayment = w.DownPayment,
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
                            ProductName = p.ProductNavigation != null ? p.ProductNavigation.ProductName : p.PartName,
                            PartName = p.PartName,
                            Quantity = p.Quantity,
                            UnitPrice = p.UnitPrice,
                            IsProvidedByCustomer = p.IsProvidedByCustomer,
                            WarrantyEndDate = p.WarrantyEndDate,
                            IsActive = p.IsActive,
                            QuotePhotoUrl = p.QuotePhotoUrl,
                            IsApproved = p.IsApproved
                        }).ToList(),
                        Services = w.Services.Select(s => new WorkOrderServiceDto
                        {
                            Id = s.Id,
                            WorkOrderId = s.WorkOrderId,
                            Description = s.Description,
                            MechanicId = s.MechanicId,
                            MechanicName = s.MechanicNavigation != null ? s.MechanicNavigation.FullName : "Sin asignar",
                            Price = s.Price,
                            EstimatedMinutes = s.EstimatedMinutes,
                            TimeUnit = s.TimeUnit,
                            WarrantyEndDate = s.WarrantyEndDate,
                            IsActive = s.IsActive,
                            IsApproved = s.IsApproved
                        }).ToList()
                    })
                    .OrderByDescending(w => w.CreatedAt)
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las órdenes de trabajo");
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
                        VehiclePlate = w.VehicleNavigation != null ? w.VehicleNavigation.Plate : null,
                        VehicleBrand = w.VehicleNavigation != null && w.VehicleNavigation.BrandNavigation != null ? w.VehicleNavigation.BrandNavigation.Name : null,
                        VehicleVersion = w.VehicleNavigation != null && w.VehicleNavigation.ModelNavigation != null ? w.VehicleNavigation.ModelNavigation.Models : null,
                        VehicleType = w.VehicleNavigation != null ? w.VehicleNavigation.VehicleType : "moto",
                        VehicleMotorization = w.VehicleNavigation != null ? w.VehicleNavigation.CylinderCapacity : null,
                        CustomerId = w.CustomerId,
                        CustomerName = w.CustomerNavigation != null ? w.CustomerNavigation.FirstName + " " + w.CustomerNavigation.LastName : null,
                        EntryDate = w.EntryDate,
                        EstimatedDeliveryDate = w.EstimatedDeliveryDate,
                        Mileage = w.Mileage,
                        FuelLevel = w.FuelLevel,
                        Observations = w.Observations,
                        Status = w.Status,
                        IsActive = w.IsActive,
                        IsBilled = _context.Sale.Any(s => s.WorkOrderId == w.Id && s.IsActive),
                        DownPayment = w.DownPayment,
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
                            ProductName = p.ProductNavigation != null ? p.ProductNavigation.ProductName : p.PartName,
                            PartName = p.PartName,
                            Quantity = p.Quantity,
                            UnitPrice = p.UnitPrice,
                            IsProvidedByCustomer = p.IsProvidedByCustomer,
                            WarrantyEndDate = p.WarrantyEndDate,
                            IsActive = p.IsActive,
                            QuotePhotoUrl = p.QuotePhotoUrl,
                            IsApproved = p.IsApproved
                        }).ToList(),
                        Services = w.Services.Select(s => new WorkOrderServiceDto
                        {
                            Id = s.Id,
                            WorkOrderId = s.WorkOrderId,
                            Description = s.Description,
                            MechanicId = s.MechanicId,
                            MechanicName = s.MechanicNavigation != null ? s.MechanicNavigation.FullName : "Sin asignar",
                            Price = s.Price,
                            EstimatedMinutes = s.EstimatedMinutes,
                            TimeUnit = s.TimeUnit,
                            WarrantyEndDate = s.WarrantyEndDate,
                            IsActive = s.IsActive,
                            IsApproved = s.IsApproved
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la orden de trabajo con ID {Id}", id);
                return null;
            }
        }

        public async Task<bool> IsBilledAsync(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                return await _context.Sale.AnyAsync(s => s.WorkOrderId == workOrderId && s.IsActive, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar si la orden {Id} está facturada", workOrderId);
                return false;
            }
        }

        public async Task<bool> HasPartsOrServicesAsync(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                bool hasParts = await _context.WorkOrderPart.AnyAsync(p => p.WorkOrderId == workOrderId && p.IsActive, cancellation);
                if (hasParts) return true;
                return await _context.WorkOrderService.AnyAsync(s => s.WorkOrderId == workOrderId && s.IsActive, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar repuestos/servicios de la orden {Id}", workOrderId);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(WorkOrder update, CancellationToken cancellation)
        {
            try
            {
                WorkOrder? existingOrder = await _context.WorkOrder
                    .Include(w => w.Parts)
                    .Include(w => w.Services)
                    .Include(w => w.Evidences)
                    .FirstOrDefaultAsync(w => w.Id == update.Id, cancellation);

                if (existingOrder == null) return false;

                int originalWorkshopId = _context.Entry(existingOrder).Property("WorkshopId").CurrentValue != null ? (int)_context.Entry(existingOrder).Property("WorkshopId").CurrentValue : 0;

                _context.Entry(existingOrder).CurrentValues.SetValues(update);
                
                if (originalWorkshopId > 0)
                {
                    _context.Entry(existingOrder).Property("WorkshopId").CurrentValue = originalWorkshopId;
                    _context.Entry(existingOrder).Property("WorkshopId").IsModified = false;
                }

                existingOrder.UpdatedAt = DateTime.Now;
                if (int.TryParse(_currentUserService.UserId, out int userId))
                    existingOrder.ResponsibleUserId = userId;

                List<(int ProductId, int Quantity, string Type, string Obs)> inventoryMovements = new List<(int ProductId, int Quantity, string Type, string Obs)>();

                foreach (WorkOrderPart existingPart in existingOrder.Parts.ToList())
                {
                    if (!update.Parts.Any(p => p.Id == existingPart.Id))
                    {
                        if (!existingPart.IsProvidedByCustomer && existingPart.ProductId.HasValue)
                            inventoryMovements.Add((existingPart.ProductId.Value, existingPart.Quantity, "Entrada", $"Cancelación de repuesto en WO #{existingOrder.Id}"));
                        _context.WorkOrderPart.Remove(existingPart);
                    }
                }

                foreach (WorkOrderPart part in update.Parts)
                {
                    WorkOrderPart? existingPart = existingOrder.Parts.FirstOrDefault(p => p.Id == part.Id && p.Id != 0);
                    if (existingPart != null)
                    {
                        if (existingPart.Quantity != part.Quantity ||
                            existingPart.UnitPrice != part.UnitPrice ||
                            existingPart.IsApproved != part.IsApproved ||
                            existingPart.PartName != part.PartName)
                        {
                            if (!existingPart.IsProvidedByCustomer && existingPart.ProductId.HasValue && existingPart.Quantity != part.Quantity)
                            {
                                int diff = part.Quantity - existingPart.Quantity;
                                string movType = diff > 0 ? "Salida" : "Entrada";
                                inventoryMovements.Add((existingPart.ProductId.Value, Math.Abs(diff), movType, $"Ajuste de cantidad en WO #{existingOrder.Id}"));
                            }
                            _context.Entry(existingPart).CurrentValues.SetValues(part);
                            existingPart.UpdatedAt = DateTime.Now;
                        }
                    }
                    else
                    {
                        part.WorkOrderId = existingOrder.Id;
                        part.CreatedAt = DateTime.Now;
                        if (userId != 0) part.ResponsibleUserId = userId;
                        existingOrder.Parts.Add(part);
                        if (!part.IsProvidedByCustomer && part.ProductId.HasValue)
                            inventoryMovements.Add((part.ProductId.Value, part.Quantity, "Salida", $"Adición de repuesto en WO #{existingOrder.Id}"));
                    }
                }

                // ─── Sincronizar Servicios ───────────────────────────────────────────────
                foreach (WorkOrderService existingService in existingOrder.Services.ToList())
                {
                    if (!update.Services.Any(s => s.Id == existingService.Id))
                        _context.WorkOrderService.Remove(existingService);
                }

                foreach (WorkOrderService service in update.Services)
                {
                    WorkOrderService? existingService = existingOrder.Services.FirstOrDefault(s => s.Id == service.Id && s.Id != 0);
                    if (existingService != null)
                    {
                        if (existingService.Price != service.Price ||
                            existingService.IsApproved != service.IsApproved ||
                            existingService.Description != service.Description ||
                            existingService.MechanicId != service.MechanicId)
                        {
                            _context.Entry(existingService).CurrentValues.SetValues(service);
                            existingService.UpdatedAt = DateTime.Now;
                        }
                    }
                    else
                    {
                        service.WorkOrderId = existingOrder.Id;
                        service.CreatedAt = DateTime.Now;
                        if (userId != 0) service.ResponsibleUserId = userId;
                        existingOrder.Services.Add(service);
                    }
                }

                // ─── Sincronizar Evidencias ──────────────────────────────────────────────
                foreach (WorkOrderEvidence existingEvidence in existingOrder.Evidences.ToList())
                {
                    if (!update.Evidences.Any(e => e.Id == existingEvidence.Id))
                        _context.WorkOrderEvidence.Remove(existingEvidence);
                }

                foreach (WorkOrderEvidence evidence in update.Evidences)
                {
                    WorkOrderEvidence? existingEvidence = existingOrder.Evidences.FirstOrDefault(e => e.Id == evidence.Id && e.Id != 0);
                    if (existingEvidence != null)
                    {
                        string originalPhotoUrl = existingEvidence.PhotoUrl;
                        _context.Entry(existingEvidence).CurrentValues.SetValues(evidence);
                        if (string.IsNullOrEmpty(existingEvidence.PhotoUrl) || existingEvidence.PhotoUrl == originalPhotoUrl)
                        {
                            existingEvidence.PhotoUrl = originalPhotoUrl;
                            _context.Entry(existingEvidence).Property(e => e.PhotoUrl).IsModified = false;
                        }
                    }
                    else
                    {
                        evidence.WorkOrderId = existingOrder.Id;
                        evidence.CreatedAt = DateTime.Now;
                        if (userId != 0) evidence.ResponsibleUserId = userId;
                        existingOrder.Evidences.Add(evidence);
                    }
                }

                // ─── Aplicar movimientos de inventario ───────────────────────────────────
                foreach ((int productId, int quantity, string type, string obs) in inventoryMovements)
                    await ApplyInventoryMovementAsync(productId, quantity, type, obs, existingOrder.Id, cancellation);

                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la orden de trabajo {Id}", update.Id);
                throw;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id, string status, string oldStatus, string actionBy, int? responsibleUserId, CancellationToken cancellation)
        {
            try
            {
                WorkOrder? workOrder = await _context.WorkOrder.FindAsync(new object[] { id }, cancellation);
                if (workOrder == null) return false;

                workOrder.Status = status;
                workOrder.UpdatedAt = DateTime.Now;
                if (responsibleUserId.HasValue)
                    workOrder.ResponsibleUserId = responsibleUserId.Value;

                await _context.WorkOrderHistory.AddAsync(new WorkOrderHistory
                {
                    WorkOrderId = id,
                    Status = status,
                    Observations = $"Cambio de estado: {oldStatus} → {status}",
                    ActionBy = actionBy,
                    ResponsibleUserId = responsibleUserId,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                }, cancellation);

                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar el estado de la orden {Id}", id);
                throw;
            }
        }

        public async Task AddHistoryEntryAsync(int workOrderId, string status, string observations, int? responsibleUserId, string actionBy, CancellationToken cancellation)
        {
            try
            {
                await _context.WorkOrderHistory.AddAsync(new WorkOrderHistory
                {
                    WorkOrderId = workOrderId,
                    Status = status,
                    Observations = observations,
                    ActionBy = actionBy,
                    ResponsibleUserId = responsibleUserId,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                }, cancellation);
                await _context.SaveChangesAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar historial en la orden {Id}", workOrderId);
            }
        }

        public async Task<IEnumerable<WorkOrderHistoryDto>> GetHistoryAsync(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                return await _context.WorkOrderHistory
                    .Where(h => h.WorkOrderId == workOrderId)
                    .OrderByDescending(h => h.CreatedAt)
                    .Select(h => new WorkOrderHistoryDto
                    {
                        Id = h.Id,
                        WorkOrderId = h.WorkOrderId,
                        Status = h.Status,
                        Observations = h.Observations,
                        ActionBy = h.ActionBy,
                        CreatedAt = h.CreatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el historial de la orden {Id}", workOrderId);
                return new List<WorkOrderHistoryDto>();
            }
        }

        public async Task<WorkOrderEvidence> AddEvidenceAsync(WorkOrderEvidence evidence, CancellationToken cancellation)
        {
            try
            {
                evidence.CreatedAt = DateTime.Now;
                evidence.IsActive = true;
                if (int.TryParse(_currentUserService.UserId, out int userId))
                    evidence.ResponsibleUserId = userId;

                await _context.WorkOrderEvidence.AddAsync(evidence, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return evidence;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar evidencia a la orden de trabajo");
                throw;
            }
        }

        public async Task<bool> DeleteEvidenceAsync(int id, CancellationToken cancellation)
        {
            try
            {
                WorkOrderEvidence? evidence = await _context.WorkOrderEvidence.FindAsync(new object[] { id }, cancellation);
                if (evidence == null) return false;

                _context.WorkOrderEvidence.Remove(evidence);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la evidencia {Id}", id);
                throw;
            }
        }

        // ─── Privado: aplica movimiento de inventario dentro del contexto ────────────
        private async Task ApplyInventoryMovementAsync(int productId, int quantity, string type, string obs, int referenceId, CancellationToken cancellation)
        {
            try
            {
                Domain.Models.Inventory? inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == productId, cancellation);
                if (inventory == null)
                {
                    inventory = new Domain.Models.Inventory
                    {
                        ProductId = productId,
                        StockQuantity = 0,
                        MinStock = 0,
                        CreatedAt = DateTime.Now,
                        IsActive = true
                    };
                    if (int.TryParse(_currentUserService.UserId, out int uid)) inventory.ResponsibleUserId = uid;
                    await _context.Inventory.AddAsync(inventory, cancellation);
                }

                if (type == "Entrada") inventory.StockQuantity += quantity;
                else if (type == "Salida") inventory.StockQuantity -= quantity;

                inventory.LastUpdate = DateTime.Now;
                inventory.UpdatedAt = DateTime.Now;

                InventoryHistory movement = new InventoryHistory
                {
                    ProductId = productId,
                    MovementType = type,
                    Quantity = quantity,
                    Observations = obs,
                    ReferenceId = referenceId,
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                if (int.TryParse(_currentUserService.UserId, out int userId)) movement.ResponsibleUserId = userId;
                await _context.InventoryHistory.AddAsync(movement, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar movimiento de inventario para producto {ProductId}", productId);
            }
        }
    }
}
