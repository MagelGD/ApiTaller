using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Dtos.ProductType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.PaymentMethods
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<GetPaymentMethodDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetPaymentMethodDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetPaymentMethodDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetPaymentMethodDto> CreateOrEditPaymentMethod(GetPaymentMethodDto paymentMethod, CancellationToken cancellationToken);
    }
}
