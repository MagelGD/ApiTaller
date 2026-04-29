using ApiTaller.Domain.Dtos.Supplier;
using ApiTaller.Domain.Interfaces.Repositories.Suppliers;
using ApiTaller.Domain.Interfaces.Services.Suppliers;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Suppliers
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<SupplierService> _logger;

        public SupplierService(ISupplierRepository supplierRepository, ILogger<SupplierService> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }
        public async Task<GetSupplierDto> CreateOrEditSupplier(GetSupplierDto supplier, CancellationToken cancellationToken)
        {
            GetSupplierDto result = new();
            try
            {
                Supplier saveData = new()
                {
                    Id = supplier.Id,
                    DocumentNumber = supplier.DocumentNumber,
                    BusinessName = supplier.BusinessName,
                    ContactName = supplier.ContactName,
                    PhoneNumber = supplier.PhoneNumber,
                    Email = supplier.Email,
                    IsActive = supplier.IsActive,
                    CreatedAt = supplier.CreatedAt ?? DateTime.Now
                };
                bool isExit = await ValidateExist(supplier, cancellationToken);
                if (saveData.Id == 0 && !isExit)
                {
                    await _supplierRepository.CreateAsync(saveData, cancellationToken);
                }
                else if (saveData.Id != 0)
                {
                    await _supplierRepository.UpdateAsync(saveData, cancellationToken);
                }
                result = await _supplierRepository.ValidateExist(supplier, cancellationToken) ?? new GetSupplierDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear o editar el proveedor con número de documento {supplier.DocumentNumber}");
            }
            return result;
        }

        public async Task<IEnumerable<GetSupplierDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetSupplierDto> result = [];
            try
            {
                result = await _supplierRepository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores activos");
            }
            return result;
        }

        public async Task<IEnumerable<GetSupplierDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetSupplierDto> result = [];
            try
            {
                result = await _supplierRepository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores");
            }
            return result;
        }

        public async Task<GetSupplierDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetSupplierDto? result = null;
            try
            {
                result = await _supplierRepository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el proveedor con id {id}");
            }
            return result;
        }

        private async Task<bool> ValidateExist(GetSupplierDto data, CancellationToken cancellation)
        {
            GetSupplierDto? result = null;
            try
            {
                result = await _supplierRepository.ValidateExist(data, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar la existencia del proveedor con número de documento {data.DocumentNumber}");
            }
            return result != null;
        }
    }
}
