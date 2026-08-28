using ApiTaller.Domain.Dtos.Credits;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Credits
{
    public interface ICreditService
    {
        Task<IEnumerable<CustomerCreditSummaryDto>> GetCustomersWithCreditAsync(CancellationToken cancellation);
        Task<CustomerCreditStatementDto?> GetCustomerStatementAsync(int customerId, CancellationToken cancellation);
        Task<bool> RegisterPaymentAsync(RegisterCreditPaymentDto dto, CancellationToken cancellation);
    }
}
