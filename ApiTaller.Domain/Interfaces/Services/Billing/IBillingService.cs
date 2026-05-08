using ApiTaller.Domain.Dtos.Billing;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Domain.Interfaces.Services.Billing
{
    public interface IBillingService
    {
        Task<bool> SaveSaleAsync(SaleDto saleDto, CancellationToken cancellation);
        Task<SaleDto> GetByWorkOrderAsync(int workOrderId, CancellationToken cancellation);
    }
}
