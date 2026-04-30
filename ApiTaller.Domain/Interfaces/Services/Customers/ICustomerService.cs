using ApiTaller.Domain.Dtos.Customer;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Customers
{
    public interface ICustomerService
    {
        Task<IEnumerable<GetCustomerDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetCustomerDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetCustomerDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetCustomerDto> CreateOrEditCustomer(GetCustomerDto customer, CancellationToken cancellationToken);
    }
}
