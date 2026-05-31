using ApiTaller.Domain.Dtos.CustomerPortal;
using ApiTaller.Domain.Interfaces.Repositories.CustomerPortal;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.CustomerPortal
{
    public class CustomerPortalRepository : ICustomerPortalRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CustomerPortalRepository> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IWorkOrderNotificationService _notificationService;

        public CustomerPortalRepository(DataContext context, ILogger<CustomerPortalRepository> logger, ICurrentUserService currentUserService, IWorkOrderNotificationService notificationService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
            _notificationService = notificationService;
        }

        // ─── Helper privado: resuelve customerId a partir del userId del JWT ───────
        private async Task<int?> GetCustomerIdFromUserAsync(CancellationToken cancellation)
        {
            if (!int.TryParse(_currentUserService.UserId, out int userId)) return null;
            var customer = await _context.Customer
                .FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive, cancellation);
            return customer?.Id;
        }

        // ─── 1. Mis vehículos ────────────────────────────────────────────────────
        public async Task<IEnumerable<CustomerPortalVehicleDto>> GetMyVehiclesAsync(CancellationToken cancellation)
        {
            try
            {
                var customerId = await GetCustomerIdFromUserAsync(cancellation);
                if (customerId == null) return new List<CustomerPortalVehicleDto>();

                var vehicles = await _context.Vehicle
                    .Include(v => v.BrandNavigation)
                    .Include(v => v.ModelNavigation)
                    .Include(v => v.VersionNavigation)
                    .Where(v => v.CustomerId == customerId && v.IsActive)
                    .ToListAsync(cancellation);

                var result = new List<CustomerPortalVehicleDto>();
                foreach (var v in vehicles)
                {
                    var orders = await _context.WorkOrder
                        .Where(o => o.VehicleId == v.Id)
                        .OrderByDescending(o => o.CreatedAt)
                        .ToListAsync(cancellation);

                    var activeAppt = await _context.Appointment
                        .FirstOrDefaultAsync(a => a.VehicleId == v.Id && a.IsActive && (a.Status == "Agendada" || a.Status == "Pendiente"), cancellation);

                    result.Add(new CustomerPortalVehicleDto
                    {
                        Id = v.Id,
                        Plate = v.Plate,
                        Brand = v.BrandNavigation?.Name ?? "",
                        Model = v.ModelNavigation?.Models ?? "",
                        Version = v.VersionNavigation?.Version ?? "",
                        Color = v.Color,
                        CylinderCapacity = v.CylinderCapacity,
                        VehicleType = v.VehicleType,
                        TotalOrders = orders.Count,
                        LastOrderStatus = orders.FirstOrDefault()?.Status,
                        LastOrderDate = orders.FirstOrDefault()?.EntryDate,
                        ActiveWorkOrderStatus = orders.FirstOrDefault(o => o.IsActive && o.Status != "Entregado" && o.Status != "Cancelada")?.Status,
                        ActiveAppointmentId = activeAppt?.Id
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer portal vehicles");
                return new List<CustomerPortalVehicleDto>();
            }
        }

        // ─── 2. Órdenes de un vehículo (verificando pertenencia) ─────────────────
        public async Task<IEnumerable<CustomerPortalOrderSummaryDto>> GetMyOrdersByVehicleAsync(int vehicleId, CancellationToken cancellation)
        {
            try
            {
                var customerId = await GetCustomerIdFromUserAsync(cancellation);
                if (customerId == null) return new List<CustomerPortalOrderSummaryDto>();

                // Verificar que el vehículo pertenece a este cliente
                var vehicleOwned = await _context.Vehicle
                    .AnyAsync(v => v.Id == vehicleId && v.CustomerId == customerId, cancellation);
                if (!vehicleOwned) return new List<CustomerPortalOrderSummaryDto>(); // Seguridad: no expone datos ajenos

                var orders = await _context.WorkOrder
                    .Include(o => o.Parts)
                    .Include(o => o.Services)
                    .Where(o => o.VehicleId == vehicleId)
                    .OrderByDescending(o => o.EntryDate)
                    .ToListAsync(cancellation);

                return orders.Select(o =>
                {
                    var totalParts = o.Parts.Where(p => p.IsActive && p.IsApproved).Sum(p => p.UnitPrice * p.Quantity);
                    var totalServices = o.Services.Where(s => s.IsActive && s.IsApproved).Sum(s => s.Price);
                    var hasPending = o.Parts.Any(p => p.IsActive && !p.IsApproved) ||
                                    o.Services.Any(s => s.IsActive && !s.IsApproved);

                    return new CustomerPortalOrderSummaryDto
                    {
                        Id = o.Id,
                        VehiclePlate = "",  // Ya conocido por el cliente, no necesario aquí
                        EntryDate = o.EntryDate,
                        EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                        Status = o.Status,
                        TotalParts = totalParts,
                        TotalServices = totalServices,
                        GrandTotal = totalParts + totalServices,
                        HasPendingApproval = hasPending && (o.Status == "Cotización" || o.Status == "Esperando Aprobación")
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting portal orders for vehicle {vehicleId}");
                return new List<CustomerPortalOrderSummaryDto>();
            }
        }

        // ─── 3. Detalle completo de una orden (verificando pertenencia) ───────────
        public async Task<CustomerPortalOrderDetailDto?> GetMyOrderDetailAsync(int orderId, CancellationToken cancellation)
        {
            try
            {
                var customerId = await GetCustomerIdFromUserAsync(cancellation);
                if (customerId == null) return null;

                // Cadena de seguridad completa: orden → vehículo → cliente
                var order = await _context.WorkOrder
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.BrandNavigation)
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.VersionNavigation)
                    .Include(o => o.Parts).ThenInclude(p => p.ProductNavigation)
                    .Include(o => o.Services)
                    .Include(o => o.Evidences)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.VehicleNavigation.CustomerId == customerId, cancellation);

                if (order == null) return null; // Orden no encontrada o no pertenece al cliente

                // Todas las fotos de evidencias (ingreso y proceso)
                var evidences = order.Evidences
                    .Where(e => e.IsActive)
                    .Select(e => new CustomerPortalEvidenceDto
                    {
                        EvidenceType = e.EvidenceType,
                        PhotoUrl = e.PhotoUrl
                    })
                    .ToList();

                var activeParts = order.Parts.Where(p => p.IsActive).ToList();
                var activeServices = order.Services.Where(s => s.IsActive).ToList();

                var approvedPartsTotal = activeParts.Where(p => p.IsApproved).Sum(p => p.UnitPrice * p.Quantity);
                var approvedServicesTotal = activeServices.Where(s => s.IsApproved).Sum(s => s.Price);

                // Historial de estados (sin exponer ActionBy)
                var history = await _context.WorkOrderHistory
                    .Where(h => h.WorkOrderId == orderId)
                    .OrderByDescending(h => h.CreatedAt)
                    .Select(h => new CustomerPortalHistoryDto
                    {
                        Status = h.Status,
                        CreatedAt = h.CreatedAt
                    })
                    .ToListAsync(cancellation);

                return new CustomerPortalOrderDetailDto
                {
                    Id = order.Id,
                    VehiclePlate = order.VehicleNavigation?.Plate ?? "",
                    VehicleBrand = order.VehicleNavigation?.BrandNavigation?.Name ?? "",
                    VehicleVersion = order.VehicleNavigation?.VersionNavigation?.Version ?? "",
                    VehicleType = order.VehicleNavigation?.VehicleType ?? "moto",
                    VehicleMotorization = order.VehicleNavigation?.CylinderCapacity ?? "",
                    EntryDate = order.EntryDate,
                    EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                    Mileage = order.Mileage,
                    FuelLevel = order.FuelLevel,
                    Observations = order.Observations,
                    Status = order.Status,
                    Evidences = evidences,
                    Parts = activeParts.Select(p => new CustomerPortalPartDto
                    {
                        Id = p.Id,
                        Name = p.ProductNavigation != null ? p.ProductNavigation.ProductName : p.PartName,
                        Quantity = p.Quantity,
                        UnitPrice = p.UnitPrice,
                        Total = p.UnitPrice * p.Quantity,
                        IsProvidedByCustomer = p.IsProvidedByCustomer,
                        IsApproved = p.IsApproved,
                        WarrantyEndDate = p.WarrantyEndDate
                    }).ToList(),
                    Services = activeServices.Select(s => new CustomerPortalServiceDto
                    {
                        Id = s.Id,
                        Description = s.Description,
                        Price = s.Price,
                        IsApproved = s.IsApproved,
                        WarrantyEndDate = s.WarrantyEndDate
                    }).ToList(),
                    History = history,
                    TotalApprovedParts = approvedPartsTotal,
                    TotalApprovedServices = approvedServicesTotal,
                    GrandTotalApproved = approvedPartsTotal + approvedServicesTotal
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting portal order detail {orderId}");
                return null;
            }
        }

        // ─── 4. Aprobar / Rechazar ítem (con verificación de pertenencia completa) ─
        public async Task<bool> ApproveItemAsync(CustomerPortalApprovalDto dto, CancellationToken cancellation)
        {
            try
            {
                var customerId = await GetCustomerIdFromUserAsync(cancellation);
                if (customerId == null) return false;

                if (dto.ItemType == "Part")
                {
                    // Verificar cadena: Part → WorkOrder → Vehicle → Customer
                    var part = await _context.WorkOrderPart
                        .Include(p => p.WorkOrderNavigation)
                            .ThenInclude(o => o.VehicleNavigation)
                        .FirstOrDefaultAsync(p => p.Id == dto.ItemId
                            && p.WorkOrderNavigation.VehicleNavigation.CustomerId == customerId
                            && p.IsActive, cancellation);

                    if (part == null) return false; // No pertenece al cliente

                    part.IsApproved = dto.IsApproved;
                    part.UpdatedAt = DateTime.Now;
                }
                else if (dto.ItemType == "Service")
                {
                    // Verificar cadena: Service → WorkOrder → Vehicle → Customer
                    var service = await _context.WorkOrderService
                        .Include(s => s.WorkOrderNavigation)
                            .ThenInclude(o => o.VehicleNavigation)
                        .FirstOrDefaultAsync(s => s.Id == dto.ItemId
                            && s.WorkOrderNavigation.VehicleNavigation.CustomerId == customerId
                            && s.IsActive, cancellation);

                    if (service == null) return false; // No pertenece al cliente

                    service.IsApproved = dto.IsApproved;
                    service.UpdatedAt = DateTime.Now;
                }
                else
                {
                    return false; // ItemType desconocido
                }

                var saved = await _context.SaveChangesAsync(cancellation) > 0;
                if (saved)
                {
                    int orderId = 0;
                    if (dto.ItemType == "Part")
                    {
                        var p = await _context.WorkOrderPart.FindAsync(dto.ItemId);
                        orderId = p?.WorkOrderId ?? 0;
                    }
                    else
                    {
                        var s = await _context.WorkOrderService.FindAsync(dto.ItemId);
                        orderId = s?.WorkOrderId ?? 0;
                    }

                    if (orderId > 0)
                    {
                        await _notificationService.NotifyWorkOrderUpdatedAsync(orderId, customerId.Value);
                    }
                }
                return saved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error approving item {dto.ItemId} ({dto.ItemType})");
                return false;
            }
        }

        public async Task<bool> CreateMyVehicleAsync(CustomerPortalCreateVehicleDto dto, CancellationToken cancellation)
        {
            try
            {
                var customerId = await GetCustomerIdFromUserAsync(cancellation);
                if (customerId == null) return false;

                var plateUpper = dto.Plate.ToUpper().Trim();
                var exists = await _context.Vehicle.AnyAsync(v => v.Plate == plateUpper, cancellation);
                if (exists)
                {
                    throw new InvalidOperationException("La placa ya se encuentra registrada en el sistema.");
                }

                var vehicle = new ApiTaller.Domain.Models.Vehicle
                {
                    Plate = plateUpper,
                    BrandId = dto.BrandId,
                    ModelId = dto.ModelId,
                    VersionId = dto.VersionId,
                    Color = dto.Color,
                    CylinderCapacity = dto.CylinderCapacity,
                    CustomerId = customerId.Value,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ResponsibleUserId = int.Parse(_currentUserService.UserId)
                };

                _context.Vehicle.Add(vehicle);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer vehicle from portal");
                return false;
            }
        }

        public async Task<IEnumerable<CustomerPortalAppointmentDto>> GetMyAppointmentsAsync(CancellationToken cancellation)
        {
            var customerId = await GetCustomerIdFromUserAsync(cancellation);
            if (customerId == null) return new List<CustomerPortalAppointmentDto>();

            return await _context.Appointment
                .Include(a => a.ServiceTypeNavigation)
                .Where(a => a.CustomerId == customerId && a.IsActive)
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new CustomerPortalAppointmentDto
                {
                    Id = a.Id,
                    AppointmentDate = a.AppointmentDate,
                    Status = a.Status,
                    VehicleDescription = a.VehicleDescription,
                    ServiceTypeName = a.ServiceTypeNavigation != null ? a.ServiceTypeNavigation.Name : "No especificado",
                    CustomerNotes = a.CustomerNotes
                })
                .ToListAsync(cancellation);
        }

        public async Task<bool> ApproveFullOrderAsync(int orderId, CancellationToken cancellation)
        {
            try
            {
                var customerId = await GetCustomerIdFromUserAsync(cancellation);
                if (customerId == null) return false;

                var order = await _context.WorkOrder
                    .Include(o => o.Parts)
                    .Include(o => o.Services)
                    .Include(o => o.VehicleNavigation).ThenInclude(v => v.CustomerNavigation)
                    .FirstOrDefaultAsync(o => o.Id == orderId && o.VehicleNavigation.CustomerId == customerId, cancellation);

                if (order == null) return false;

                // Solo permitir si está en estados de aprobación/cotización
                var s = order.Status ?? "";
                var validStatuses = new[] { "Cotización", "Cotizacion", "En Aprobación", "En Aprobacion", "Recepción", "Recepcion", "Ingreso" };
                
                if (!validStatuses.Any(vs => vs.Equals(s, StringComparison.OrdinalIgnoreCase) || s.Contains(vs, StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning("Intento de aprobación total en estado no permitido: {Status}", s);
                    return false;
                }

                // 1. Aprobar todo lo activo
                foreach (var p in order.Parts.Where(p => p.IsActive)) p.IsApproved = true;
                foreach (var svc in order.Services.Where(s => s.IsActive)) svc.IsApproved = true;

                // 2. Cambiar estado
                order.Status = "Aprobado";
                order.UpdatedAt = DateTime.Now;

                // 3. Registrar historia
                var history = new ApiTaller.Domain.Models.WorkOrderHistory
                {
                    WorkOrderId = order.Id,
                    Status = "Aprobado",
                    Observations = "Presupuesto aprobado por el cliente desde el portal.",
                    ActionBy = order.VehicleNavigation?.CustomerNavigation?.FirstName ?? "Cliente (Portal)",
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                if (int.TryParse(_currentUserService.UserId, out int userId)) history.ResponsibleUserId = userId;
                
                _context.WorkOrderHistory.Add(history);

                var saved = await _context.SaveChangesAsync(cancellation) > 0;
                if (saved)
                {
                    await _notificationService.NotifyWorkOrderUpdatedAsync(orderId, customerId.Value);
                }
                return saved;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error approving full order {orderId} in portal");
                return false;
            }
        }
    }
}
