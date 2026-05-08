using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Repositories.Billing;
using ApiTaller.Domain.Interfaces.Services.Billing;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Application.Services.Billing
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _billingRepository;

        public BillingService(IBillingRepository billingRepository)
        {
            _billingRepository = billingRepository;
        }

        public async Task<bool> SaveSaleAsync(SaleDto saleDto, CancellationToken cancellation)
        {
            return await _billingRepository.SaveSaleAsync(saleDto, cancellation);
        }
    }
}
