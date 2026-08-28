using ApiTaller.Domain.Dtos.Credits;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Credits
{
    public interface ICreditRepository
    {
        Task<IEnumerable<CustomerCreditSummaryDto>> GetCustomersWithCreditAsync(CancellationToken cancellation);
        Task<CustomerCreditStatementDto?> GetCustomerStatementAsync(int customerId, CancellationToken cancellation);
        Task<bool> RegisterPaymentAsync(RegisterCreditPaymentDto dto, CancellationToken cancellation);
    }
}
