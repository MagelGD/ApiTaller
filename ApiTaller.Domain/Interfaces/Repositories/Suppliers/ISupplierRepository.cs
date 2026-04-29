using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Dtos.Supplier;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Suppliers
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<GetSupplierDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetSupplierDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetSupplierDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(Supplier create, CancellationToken cancellation);
        Task<bool> UpdateAsync(Supplier update, CancellationToken cancellation);
        Task<GetSupplierDto?> ValidateExist(GetSupplierDto type, CancellationToken cancellation);
    }
}
