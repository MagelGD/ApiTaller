using ApiTaller.Domain.Dtos;
using ApiTaller.Domain.Dtos.WorkOrder;
using ApiTaller.Domain.Interfaces.Repositories.WorkOrders;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.WorkOrders
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IWorkOrderNotificationService _notificationService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<WorkOrderService> _logger;

        public WorkOrderService(
            IWorkOrderRepository workOrderRepository,
            IWorkOrderNotificationService notificationService,
            ICurrentUserService currentUserService,
            ILogger<WorkOrderService> logger)
        {
            _workOrderRepository = workOrderRepository;
            _notificationService = notificationService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<IEnumerable<WorkOrderDto>> GetAllAsync(string? vehicleType, CancellationToken cancellation)
        {
            try
            {
                return await _workOrderRepository.GetAllAsync(vehicleType, cancellation);
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
                return await _workOrderRepository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la orden de trabajo {Id}", id);
                return null;
            }
        }

        public async Task<bool> SaveAsync(WorkOrderDto dto, CancellationToken cancellation)
        {
            try
            {
                // ─── Construir el modelo de dominio ──────────────────────────────────────
                WorkOrder model = new()
                {
                    Id = dto.Id,
                    VehicleId = dto.VehicleId,
                    CustomerId = dto.CustomerId,
                    EntryDate = dto.EntryDate,
                    EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
                    Mileage = dto.Mileage,
                    FuelLevel = dto.FuelLevel,
                    Observations = dto.Observations,
                    Status = dto.Status,
                    DownPayment = dto.DownPayment,
                    IsActive = dto.IsActive,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = DateTime.Now
                };

                if (dto.Parts != null)
                {
                    foreach (WorkOrderPartDto part in dto.Parts)
                    {
                        model.Parts.Add(new WorkOrderPart
                        {
                            Id = part.Id,
                            WorkOrderId = dto.Id,
                            ProductId = part.ProductId,
                            PartName = part.PartName,
                            Quantity = part.Quantity,
                            UnitPrice = part.UnitPrice,
                            IsProvidedByCustomer = part.IsProvidedByCustomer,
                            WarrantyEndDate = part.WarrantyEndDate,
                            QuotePhotoUrl = part.QuotePhotoUrl,
                            IsApproved = part.IsApproved,
                            IsActive = part.IsActive,
                            CreatedAt = part.Id == 0 ? DateTime.Now : dto.CreatedAt,
                            UpdatedAt = DateTime.Now
                        });
                    }
                }

                if (dto.Services != null)
                {
                    foreach (WorkOrderServiceDto service in dto.Services)
                    {
                        model.Services.Add(new Domain.Models.WorkOrderService
                        {
                            Id = service.Id,
                            WorkOrderId = dto.Id,
                            Description = service.Description,
                            MechanicId = service.MechanicId,
                            Price = service.Price,
                            EstimatedMinutes = service.EstimatedMinutes,
                            TimeUnit = service.TimeUnit,
                            WarrantyEndDate = service.WarrantyEndDate,
                            IsApproved = service.IsApproved,
                            IsActive = service.IsActive,
                            CreatedAt = service.Id == 0 ? DateTime.Now : dto.CreatedAt,
                            UpdatedAt = DateTime.Now
                        });
                    }
                }

                if (dto.Evidences != null)
                {
                    foreach (WorkOrderEvidenceDto evidence in dto.Evidences)
                    {
                        model.Evidences.Add(new WorkOrderEvidence
                        {
                            Id = evidence.Id,
                            WorkOrderId = dto.Id,
                            PhotoUrl = evidence.PhotoUrl,
                            EvidenceType = evidence.EvidenceType,
                            Description = evidence.Description,
                            IsActive = evidence.IsActive,
                            CreatedAt = evidence.Id == 0 ? DateTime.Now : dto.CreatedAt,
                            UpdatedAt = DateTime.Now
                        });
                    }
                }

                bool result;

                if (model.Id == 0)
                {
                    // ─── Creación nueva ───────────────────────────────────────────────────
                    result = await _workOrderRepository.CreateAsync(model, cancellation);

                    if (result)
                    {
                        // Registrar historial de creación
                        string userName = await ResolveCurrentUserNameAsync(cancellation);
                        int? userId = ResolveCurrentUserId();
                        await _workOrderRepository.AddHistoryEntryAsync(
                            model.Id,
                            model.Status,
                            $"Orden de trabajo creada con estado inicial: {model.Status}",
                            userId,
                            userName,
                            cancellation);
                    }
                }
                else
                {
                    // ─── Actualización ────────────────────────────────────────────────────

                    // REGLA DE NEGOCIO: No modificar una orden facturada o entregada
                    bool isBilled = await _workOrderRepository.IsBilledAsync(model.Id, cancellation);
                    Domain.Models.WorkOrder? existing = await _workOrderRepository.GetByIdAsync(model.Id, cancellation);

                    if (existing != null)
                    {
                        bool isDelivered = existing.Status.Equals("Entregado", StringComparison.OrdinalIgnoreCase);

                        if (isBilled || isDelivered)
                        {
                            if (existing.EstimatedDeliveryDate != dto.EstimatedDeliveryDate ||
                                existing.Observations != dto.Observations ||
                                existing.Mileage != dto.Mileage ||
                                existing.FuelLevel != dto.FuelLevel ||
                                existing.CustomerId != dto.CustomerId ||
                                existing.VehicleId != dto.VehicleId ||
                                existing.EntryDate != dto.EntryDate)
                            {
                                throw new InvalidOperationException("No se pueden modificar los datos de una orden de trabajo que ya ha sido entregada o facturada.");
                            }
                        }

                        // REGLA DE NEGOCIO: No inactivar una orden facturada/terminada/entregada
                        if (!dto.IsActive && existing.IsActive)
                        {
                            if (isBilled)
                                throw new InvalidOperationException("No se puede inactivar una orden de trabajo que ya ha sido facturada.");

                            if (existing.Status.Equals("Terminado", StringComparison.OrdinalIgnoreCase) ||
                                existing.Status.Equals("Entregado", StringComparison.OrdinalIgnoreCase))
                                throw new InvalidOperationException("No se puede inactivar una orden de trabajo que ya ha sido terminada o entregada.");
                        }

                        // REGLA DE NEGOCIO: No cambiar estado si ya está facturada
                        if (existing.Status != dto.Status && isBilled)
                            throw new InvalidOperationException("No se puede cambiar el estado de una orden de trabajo que ya ha sido facturada.");

                        // REGLA DE NEGOCIO: No pasar a aprobación sin repuestos ni servicios
                        if (dto.Status.Equals("En Aprobación", StringComparison.OrdinalIgnoreCase) ||
                            dto.Status.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
                        {
                            bool hasItems = await _workOrderRepository.HasPartsOrServicesAsync(model.Id, cancellation);
                            if (!hasItems && (dto.Parts == null || dto.Parts.Count == 0) && (dto.Services == null || dto.Services.Count == 0))
                                throw new InvalidOperationException("No es posible pasar a aprobación o aprobado una orden de trabajo que no posee repuestos ni servicios registrados.");
                        }
                    }

                    result = await _workOrderRepository.UpdateAsync(model, cancellation);

                    // ─── Registrar historial de actualización ─────────────────────────────
                    if (result && dto.Parts != null)
                    {
                        string userName = await ResolveCurrentUserNameAsync(cancellation);
                        int? userId = ResolveCurrentUserId();
                        string message = BuildUpdateHistoryMessage(dto);
                        await _workOrderRepository.AddHistoryEntryAsync(
                            model.Id,
                            model.Status,
                            message,
                            userId,
                            userName,
                            cancellation);
                    }
                }

                if (result)
                    await _notificationService.NotifyWorkOrderUpdatedAsync(model.Id, model.CustomerId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la orden de trabajo");
                throw;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id, string status, CancellationToken cancellation)
        {
            try
            {
                // ─── Obtener estado actual ────────────────────────────────────────────────
                Domain.Models.WorkOrder? existing = await _workOrderRepository.GetByIdAsync(id, cancellation);
                if (existing == null) return false;

                string oldStatus = existing.Status;
                if (oldStatus == status) return true;

                // REGLA DE NEGOCIO: No cambiar estado si ya está facturada
                bool isBilled = await _workOrderRepository.IsBilledAsync(id, cancellation);
                if (isBilled)
                    throw new InvalidOperationException("No se puede cambiar el estado de una orden de trabajo que ya ha sido facturada.");

                // REGLA DE NEGOCIO: No pasar a aprobación/aprobado sin repuestos ni servicios
                if (status.Equals("En Aprobación", StringComparison.OrdinalIgnoreCase) ||
                    status.Equals("Aprobado", StringComparison.OrdinalIgnoreCase))
                {
                    bool hasItems = await _workOrderRepository.HasPartsOrServicesAsync(id, cancellation);
                    if (!hasItems)
                        throw new InvalidOperationException("No es posible pasar a aprobación o aprobado una orden de trabajo que no posee repuestos ni servicios registrados.");
                }

                // Resolver datos del usuario responsable
                string userName = await ResolveCurrentUserNameAsync(cancellation);
                int? userId = ResolveCurrentUserId();

                bool success = await _workOrderRepository.ChangeStatusAsync(id, status, oldStatus, userName, userId, cancellation);

                if (success)
                    await _notificationService.NotifyWorkOrderUpdatedAsync(id, existing.CustomerId);

                return success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar el estado de la orden {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<WorkOrderHistoryDto>> GetHistoryAsync(int workOrderId, CancellationToken cancellation)
        {
            try
            {
                return await _workOrderRepository.GetHistoryAsync(workOrderId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el historial de la orden {Id}", workOrderId);
                return new List<WorkOrderHistoryDto>();
            }
        }

        public async Task<WorkOrderEvidenceDto> AddEvidenceAsync(WorkOrderEvidenceDto dto, CancellationToken cancellation)
        {
            try
            {
                WorkOrderEvidence model = new WorkOrderEvidence
                {
                    WorkOrderId = dto.WorkOrderId,
                    FileName = dto.FileName,
                    FileUrl = dto.FileUrl,
                    FileType = dto.FileType,
                    IsActive = dto.IsActive,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = dto.ResponsibleUserId
                };

                WorkOrderEvidence savedModel = await _workOrderRepository.AddEvidenceAsync(model, cancellation);

                return new WorkOrderEvidenceDto
                {
                    Id = savedModel.Id,
                    WorkOrderId = savedModel.WorkOrderId,
                    PhotoUrl = savedModel.PhotoUrl,
                    EvidenceType = savedModel.EvidenceType,
                    Description = savedModel.Description,
                    IsActive = savedModel.IsActive
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar evidencia en el servicio");
                throw;
            }
        }

        public async Task<bool> DeleteEvidenceAsync(int id, CancellationToken cancellation)
        {
            try
            {
                return await _workOrderRepository.DeleteEvidenceAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la evidencia {Id}", id);
                throw;
            }
        }

        // ─── Helpers privados ────────────────────────────────────────────────────────

        private int? ResolveCurrentUserId()
        {
            return int.TryParse(_currentUserService.UserId, out int id) ? id : null;
        }

        private async Task<string> ResolveCurrentUserNameAsync(CancellationToken cancellation)
        {
            // El servicio no accede al contexto directamente; retorna el ID como texto.
            // Si se requiere el nombre completo, inyectar IUserRepository.
            return _currentUserService.UserId ?? "Sistema";
        }

        private static string BuildUpdateHistoryMessage(WorkOrderDto dto)
        {
            StringBuilder msg = new StringBuilder("Actualización de la orden:");

            int partsAdded   = dto.Parts?.FindAll(p => p.Id == 0).Count ?? 0;
            int servicesAdded = dto.Services?.FindAll(s => s.Id == 0).Count ?? 0;

            if (partsAdded > 0)    msg.Append($"\n- Se agregaron {partsAdded} repuestos.");
            if (servicesAdded > 0) msg.Append($"\n- Se agregaron {servicesAdded} servicios.");

            if (partsAdded == 0 && servicesAdded == 0)
                msg.Append("\n- Información general actualizada.");

            return msg.ToString();
        }
    }
}
