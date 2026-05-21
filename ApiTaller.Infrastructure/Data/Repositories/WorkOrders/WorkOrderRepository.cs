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
                string userName = "Sistema";
                int userId = 0;
                if (int.TryParse(_currentUserService.UserId, out userId))
                {
                    create.ResponsibleUserId = userId;
                    var user = await _context.User.FindAsync(userId);
                    if (user != null) userName = user.FullName;
                }
                create.CreatedAt = DateTime.Now;
                create.IsActive = true;

                if (userId != 0)
                {
                    foreach (var p in create.Parts) p.ResponsibleUserId = userId;
                    foreach (var s in create.Services) s.ResponsibleUserId = userId;
                    foreach (var e in create.Evidences) e.ResponsibleUserId = userId;
                }

                await _context.WorkOrder.AddAsync(create, cancellation);
                // Primer SaveChanges: genera el Id de la orden
                var saved = await _context.SaveChangesAsync(cancellation) > 0;
                if (!saved) return false;

                // Segundo paso: registrar historial con el Id ya generado
                await RegisterHistoryEntryAsync(
                    create.Id,
                    create.Status,
                    $"Orden de trabajo creada con estado inicial: {create.Status}",
                    userId != 0 ? userId : null,
                    userName,
                    cancellation);
                await _context.SaveChangesAsync(cancellation);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating work order");
                throw;
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
                        IsBilled = _context.Sale.Any(s => s.WorkOrderId == w.Id && s.IsActive),
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
                        IsBilled = _context.Sale.Any(s => s.WorkOrderId == w.Id && s.IsActive),
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
                _logger.LogError(ex, $"Error getting work order by id {id}");
                return null;
            }
        }

        public async Task<bool> UpdateAsync(WorkOrder update, CancellationToken cancellation)
        {
            try
            {
                var existingOrder = await _context.WorkOrder
                    .Include(w => w.Parts)
                    .Include(w => w.Services)
                    .Include(w => w.Evidences)
                    .FirstOrDefaultAsync(w => w.Id == update.Id, cancellation);

                if (existingOrder == null) return false;

                // Validar que no se modifique una orden de trabajo que ya ha sido entregada o facturada
                bool isBilledBeforeUpdate = await _context.Sale.AnyAsync(s => s.WorkOrderId == update.Id && s.IsActive, cancellation);
                bool isDeliveredBeforeUpdate = existingOrder.Status.Equals("Entregado", StringComparison.OrdinalIgnoreCase);

                if (isBilledBeforeUpdate || isDeliveredBeforeUpdate)
                {
                    if (existingOrder.EstimatedDeliveryDate != update.EstimatedDeliveryDate ||
                        existingOrder.Observations != update.Observations ||
                        existingOrder.Mileage != update.Mileage ||
                        existingOrder.FuelLevel != update.FuelLevel ||
                        existingOrder.CustomerId != update.CustomerId ||
                        existingOrder.VehicleId != update.VehicleId ||
                        existingOrder.EntryDate != update.EntryDate ||
                        existingOrder.DownPayment != update.DownPayment)
                    {
                        throw new InvalidOperationException("No se pueden modificar los datos de una orden de trabajo que ya ha sido entregada o facturada.");
                    }
                }

                // Validar que no se inactive una orden facturada, terminada o entregada
                if (!update.IsActive && existingOrder.IsActive)
                {
                    bool isBilled = await _context.Sale.AnyAsync(s => s.WorkOrderId == update.Id && s.IsActive, cancellation);
                    if (isBilled)
                    {
                        throw new InvalidOperationException("No se puede inactivar una orden de trabajo que ya ha sido facturada.");
                    }

                    if (existingOrder.Status.Equals("Terminado", StringComparison.OrdinalIgnoreCase) || 
                        existingOrder.Status.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidOperationException("No se puede inactivar una orden de trabajo que ya ha sido terminada o entregada.");
                    }
                }

                // Validar que no se cambie el estado si ya está facturada
                if (existingOrder.Status != update.Status)
                {
                    bool isBilled = await _context.Sale.AnyAsync(s => s.WorkOrderId == update.Id && s.IsActive, cancellation);
                    if (isBilled)
                    {
                        throw new InvalidOperationException("No se puede cambiar el estado de una orden de trabajo que ya ha sido facturada.");
                    }
                }

                // Validar que no se pase a En Aprobación o Aprobado sin repuestos ni servicios
                if (update.Status.Equals("En Aprobación", StringComparison.OrdinalIgnoreCase) || update.Status.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
                {
                    if ((update.Parts == null || update.Parts.Count == 0) && (update.Services == null || update.Services.Count == 0))
                    {
                        throw new InvalidOperationException("No es posible pasar a aprobación o aprobado una orden de trabajo que no posee repuestos ni servicios registrados.");
                    }
                }

                // Actualizar cabecera
                _context.Entry(existingOrder).CurrentValues.SetValues(update);
                existingOrder.UpdatedAt = DateTime.Now;
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    existingOrder.ResponsibleUserId = userId;
                }

                int partsAdded = 0, partsRemoved = 0, partsUpdated = 0;
                int servicesAdded = 0, servicesRemoved = 0, servicesUpdated = 0;
                int evidencesAdded = 0, evidencesRemoved = 0;

                // Sincronizar Repuestos (Parts) con Inventario
                foreach (var existingPart in existingOrder.Parts.ToList())
                {
                    if (!update.Parts.Any(p => p.Id == existingPart.Id))
                    {
                        partsRemoved++;
                        // Si era un repuesto del taller, devolver al inventario
                        if (!existingPart.IsProvidedByCustomer && existingPart.ProductId.HasValue)
                        {
                            await RegisterInventoryMovement(existingPart.ProductId.Value, existingPart.Quantity, "Entrada", $"Cancelación de repuesto en WO #{existingOrder.Id}", existingOrder.Id, cancellation);
                        }
                        _context.WorkOrderPart.Remove(existingPart);
                    }
                }

                foreach (var part in update.Parts)
                {
                    var existingPart = existingOrder.Parts.FirstOrDefault(p => p.Id == part.Id && p.Id != 0);
                    if (existingPart != null)
                    {
                        // Solo marcar como actualizado si hubo cambios reales
                        if (existingPart.Quantity != part.Quantity || 
                            existingPart.UnitPrice != part.UnitPrice || 
                            existingPart.IsApproved != part.IsApproved ||
                            existingPart.PartName != part.PartName)
                        {
                            partsUpdated++;
                            // Si cambió la cantidad y es del taller, ajustar inventario
                            if (!existingPart.IsProvidedByCustomer && existingPart.ProductId.HasValue && existingPart.Quantity != part.Quantity)
                            {
                                int diff = part.Quantity - existingPart.Quantity;
                                string type = diff > 0 ? "Salida" : "Entrada";
                                await RegisterInventoryMovement(existingPart.ProductId.Value, Math.Abs(diff), type, $"Ajuste de cantidad en WO #{existingOrder.Id}", existingOrder.Id, cancellation);
                            }

                            _context.Entry(existingPart).CurrentValues.SetValues(part);
                            existingPart.UpdatedAt = DateTime.Now;
                        }
                    }
                    else
                    {
                        partsAdded++;
                        part.WorkOrderId = existingOrder.Id;
                        part.CreatedAt = DateTime.Now;
                        if (userId != 0) part.ResponsibleUserId = userId;
                        existingOrder.Parts.Add(part);

                        // Si es repuesto del taller, descontar
                        if (!part.IsProvidedByCustomer && part.ProductId.HasValue)
                        {
                            await RegisterInventoryMovement(part.ProductId.Value, part.Quantity, "Salida", $"Adición de repuesto en WO #{existingOrder.Id}", existingOrder.Id, cancellation);
                        }
                    }
                }

                // Sincronizar Servicios (Services)
                foreach (var existingService in existingOrder.Services.ToList())
                {
                    if (!update.Services.Any(s => s.Id == existingService.Id))
                    {
                        servicesRemoved++;
                        _context.WorkOrderService.Remove(existingService);
                    }
                }
                foreach (var service in update.Services)
                {
                    var existingService = existingOrder.Services.FirstOrDefault(s => s.Id == service.Id && s.Id != 0);
                    if (existingService != null)
                    {
                        // Solo marcar como actualizado si hubo cambios reales
                        if (existingService.Price != service.Price || 
                            existingService.IsApproved != service.IsApproved ||
                            existingService.Description != service.Description ||
                            existingService.MechanicId != service.MechanicId)
                        {
                            servicesUpdated++;
                            _context.Entry(existingService).CurrentValues.SetValues(service);
                            existingService.UpdatedAt = DateTime.Now;
                        }
                    }
                    else
                    {
                        servicesAdded++;
                        service.WorkOrderId = existingOrder.Id;
                        service.CreatedAt = DateTime.Now;
                        if (userId != 0) service.ResponsibleUserId = userId;
                        existingOrder.Services.Add(service);
                    }
                }

                // Sincronizar Evidencias con detalle por tipo
                var addedByType = new Dictionary<string, int>();
                var removedByType = new Dictionary<string, int>();

                foreach (var existingEvidence in existingOrder.Evidences.ToList())
                {
                    if (!update.Evidences.Any(e => e.Id == existingEvidence.Id))
                    {
                        var type = existingEvidence.EvidenceType ?? "General";
                        if (!removedByType.ContainsKey(type)) removedByType[type] = 0;
                        removedByType[type]++;
                        _context.WorkOrderEvidence.Remove(existingEvidence);
                    }
                }
                foreach (var evidence in update.Evidences)
                {
                    var existingEvidence = existingOrder.Evidences.FirstOrDefault(e => e.Id == evidence.Id && e.Id != 0);
                    if (existingEvidence != null)
                    {
                        var originalPhotoUrl = existingEvidence.PhotoUrl;
                        _context.Entry(existingEvidence).CurrentValues.SetValues(evidence);
                        if (string.IsNullOrEmpty(existingEvidence.PhotoUrl) || existingEvidence.PhotoUrl == originalPhotoUrl)
                        {
                            existingEvidence.PhotoUrl = originalPhotoUrl;
                            _context.Entry(existingEvidence).Property(e => e.PhotoUrl).IsModified = false;
                        }
                    }
                    else
                    {
                        var type = evidence.EvidenceType ?? "General";
                        if (!addedByType.ContainsKey(type)) addedByType[type] = 0;
                        addedByType[type]++;

                        evidence.WorkOrderId = existingOrder.Id;
                        evidence.CreatedAt = DateTime.Now;
                        if (userId != 0) evidence.ResponsibleUserId = userId;
                        existingOrder.Evidences.Add(evidence);
                    }
                }

                // Registrar historial de actualización
                int histUserId = 0;
                string histUserName = "Sistema";
                if (int.TryParse(_currentUserService.UserId, out histUserId))
                {
                    var histUser = await _context.User.FindAsync(histUserId);
                    if (histUser != null) histUserName = histUser.FullName;
                }

                var msgBuilder = new System.Text.StringBuilder("Actualización de la orden:");
                if (partsAdded > 0) msgBuilder.Append($"\n- Se agregaron {partsAdded} repuestos.");
                if (partsRemoved > 0) msgBuilder.Append($"\n- Se eliminaron {partsRemoved} repuestos.");
                if (partsUpdated > 0) msgBuilder.Append($"\n- Se actualizaron {partsUpdated} repuestos.");
                if (servicesAdded > 0) msgBuilder.Append($"\n- Se agregaron {servicesAdded} servicios.");
                if (servicesRemoved > 0) msgBuilder.Append($"\n- Se eliminaron {servicesRemoved} servicios.");
                if (servicesUpdated > 0) msgBuilder.Append($"\n- Se actualizaron {servicesUpdated} servicios.");

                // Detalle de evidencias por tipo
                foreach (var kvp in addedByType) msgBuilder.Append($"\n- Se agregaron {kvp.Value} evidencias de {kvp.Key}.");
                foreach (var kvp in removedByType) msgBuilder.Append($"\n- Se eliminaron {kvp.Value} evidencias de {kvp.Key}.");

                if (partsAdded == 0 && partsRemoved == 0 && partsUpdated == 0 && 
                    servicesAdded == 0 && servicesRemoved == 0 && servicesUpdated == 0 && 
                    addedByType.Count == 0 && removedByType.Count == 0)
                {
                    msgBuilder.Append("\n- Información general actualizada.");
                }

                await RegisterHistoryEntryAsync(
                    existingOrder.Id,
                    existingOrder.Status,
                    msgBuilder.ToString(),
                    histUserId != 0 ? histUserId : null,
                    histUserName,
                    cancellation);

                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating work order with children");
                throw;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id, string status, CancellationToken cancellation)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellation);
            try
            {
                var workOrder = await _context.WorkOrder.FindAsync(new object[] { id }, cancellation);
                if (workOrder == null) return false;

                var oldStatus = workOrder.Status;
                if (oldStatus == status) return true;

                // VALIDACIÓN DE NEGOCIO: Evitar cambiar el estado si la orden ya ha sido facturada
                bool isBilled = await _context.Sale.AnyAsync(s => s.WorkOrderId == id && s.IsActive, cancellation);
                if (isBilled)
                {
                    throw new InvalidOperationException("No se puede cambiar el estado de una orden de trabajo que ya ha sido facturada.");
                }

                // VALIDACIÓN DE NEGOCIO: Evitar pasar a En Aprobación o Aprobado sin repuestos ni servicios
                if (status.Equals("En Aprobación", StringComparison.OrdinalIgnoreCase) || status.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
                {
                    bool hasParts = await _context.WorkOrderPart.AnyAsync(p => p.WorkOrderId == id && p.IsActive, cancellation);
                    bool hasServices = await _context.WorkOrderService.AnyAsync(s => s.WorkOrderId == id && s.IsActive, cancellation);
                    if (!hasParts && !hasServices)
                    {
                        throw new InvalidOperationException("No es posible pasar a aprobación o aprobado una orden de trabajo que no posee repuestos ni servicios registrados.");
                    }
                }

                workOrder.Status = status;
                workOrder.UpdatedAt = DateTime.Now;

                string userName = "Sistema";
                int userId = 0;
                if (int.TryParse(_currentUserService.UserId, out userId))
                {
                    workOrder.ResponsibleUserId = userId;
                    var user = await _context.User.FindAsync(userId);
                    if (user != null) userName = user.FullName;
                }

                // Registrar historial usando helper centralizado
                await RegisterHistoryEntryAsync(
                    id,
                    status,
                    $"Cambio de estado: {oldStatus} → {status}",
                    userId != 0 ? userId : null,
                    userName,
                    cancellation);

                var result = await _context.SaveChangesAsync(cancellation) > 0;
                await transaction.CommitAsync(cancellation);
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellation);
                _logger.LogError(ex, "Error changing work order status with history");
                throw;
            }
        }

        public async Task<IEnumerable<WorkOrderHistoryDto>> GetHistoryAsync(int workOrderId, CancellationToken cancellation)
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

        // ─── Helper centralizado: registra una entrada de historial ──────────────
        private async Task RegisterHistoryEntryAsync(
            int workOrderId,
            string status,
            string observations,
            int? responsibleUserId,
            string actionBy,
            CancellationToken cancellation)
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
        }

        private async Task RegisterInventoryMovement(int productId, int quantity, string type, string obs, int referenceId, CancellationToken cancellation)
        {
            var inventory = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == productId, cancellation);
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

            var movement = new InventoryHistory
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

        public async Task<WorkOrderEvidence> AddEvidenceAsync(WorkOrderEvidence evidence, CancellationToken cancellation)
        {
            try
            {
                evidence.CreatedAt = DateTime.Now;
                evidence.IsActive = true;
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    evidence.ResponsibleUserId = userId;
                }

                await _context.WorkOrderEvidence.AddAsync(evidence, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return evidence;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la evidencia individual");
                throw;
            }
        }

        public async Task<bool> DeleteEvidenceAsync(int id, CancellationToken cancellation)
        {
            try
            {
                var evidence = await _context.WorkOrderEvidence.FindAsync(new object[] { id }, cancellation);
                if (evidence == null) return false;

                _context.WorkOrderEvidence.Remove(evidence);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la evidencia individual con id {id}");
                throw;
            }
        }
    }
}
