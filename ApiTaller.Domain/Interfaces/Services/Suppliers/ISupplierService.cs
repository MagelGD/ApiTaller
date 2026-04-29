using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Dtos.Supplier;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Suppliers
{
    public interface ISupplierService
    {
        Task<IEnumerable<GetSupplierDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetSupplierDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetSupplierDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetSupplierDto> CreateOrEditSupplier(GetSupplierDto supplier, CancellationToken cancellationToken);
    }
}
