using ApiTaller.Domain.Dtos.Customer;
using ApiTaller.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Customers
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<GetCustomerDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetCustomerDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetCustomerDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetCustomerDto?> ValidateExist(GetCustomerDto data, CancellationToken cancellation);
        Task<bool> CreateAsync(Customer create, CancellationToken cancellation);
        Task<bool> UpdateAsync(Customer update, CancellationToken cancellation);
        Task<(bool HasActive, int WorkOrderId, string? Plate, string Status)?> GetActiveWorkOrderInfoAsync(int customerId, CancellationToken cancellation);
    }
}

