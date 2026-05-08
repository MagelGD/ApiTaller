using ApiTaller.Domain.Dtos.Billing;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Repositories.Billing
{
    public interface IBillingRepository
    {
        Task<bool> SaveSaleAsync(SaleDto saleDto, CancellationToken cancellation);
        Task<SaleDto> GetByWorkOrderAsync(int workOrderId, CancellationToken cancellation);
    }
}
