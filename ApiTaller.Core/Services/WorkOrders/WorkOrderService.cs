using ApiTaller.Domain.Dtos;
using ApiTaller.Domain.Dtos.WorkOrder;
using ApiTaller.Domain.Interfaces.Repositories.WorkOrders;
using ApiTaller.Domain.Interfaces.Services.WorkOrders;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.WorkOrders
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ILogger<WorkOrderService> _logger;

        public WorkOrderService(IWorkOrderRepository workOrderRepository, ILogger<WorkOrderService> logger)
        {
            _workOrderRepository = workOrderRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<WorkOrderDto>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _workOrderRepository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all work orders in service");
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
                _logger.LogError(ex, $"Error getting work order {id} in service");
                return null;
            }
        }

        public async Task<bool> SaveAsync(WorkOrderDto dto, CancellationToken cancellation)
        {
            try
            {
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
                    IsActive = dto.IsActive,
                    CreatedAt = dto.CreatedAt,
                    UpdatedAt = DateTime.Now
                };

                if (dto.Parts != null)
                {
                    foreach (var part in dto.Parts)
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
                            CreatedAt = part.Id == 0 ? DateTime.Now : dto.CreatedAt, // Si es nuevo usamos ahora, si no, conservamos (aprox)
                            UpdatedAt = DateTime.Now
                        });
                    }
                }

                if (dto.Services != null)
                {
                    foreach (var service in dto.Services)
                    {
                        model.Services.Add(new Domain.Models.WorkOrderService
                        {
                            Id = service.Id,
                            WorkOrderId = dto.Id,
                            Description = service.Description,
                            MechanicId = service.MechanicId,
                            Price = service.Price,
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
                    foreach (var evidence in dto.Evidences)
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

                if (model.Id == 0)
                {
                    return await _workOrderRepository.CreateAsync(model, cancellation);
                }
                else
                {
                    return await _workOrderRepository.UpdateAsync(model, cancellation);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving work order in service");
                return false;
            }
        }

        public async Task<bool> ChangeStatusAsync(int id, string status, CancellationToken cancellation)
        {
            try
            {
                return await _workOrderRepository.ChangeStatusAsync(id, status, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing status for work order {id}");
                return false;
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
                _logger.LogError(ex, $"Error getting history for work order {workOrderId}");
                return new List<WorkOrderHistoryDto>();
            }
        }
    }
}
