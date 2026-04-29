using ApiTaller.Domain.Dtos.PaymentMethod;
using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.PaymentMethods
{
    public interface IPaymentMethosRepository
    {
        Task<IEnumerable<GetPaymentMethodDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetPaymentMethodDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetPaymentMethodDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(PaymentMethod create, CancellationToken cancellation);
        Task<bool> UpdateAsync(PaymentMethod update, CancellationToken cancellation);
        Task<GetPaymentMethodDto?> ValidateExist(string type, CancellationToken cancellation);
    }
}
