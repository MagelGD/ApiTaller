using ApiTaller.Domain.Dtos.ProductType;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.ProductTypes
{
    public interface IProductTypeRepository
    {
        Task<IEnumerable<GetProductTypeDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetProductTypeDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetProductTypeDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(ProductType create, CancellationToken cancellation);
        Task<bool> UpdateAsync(ProductType update, CancellationToken cancellation);
        Task<GetProductTypeDto?> ValidateExist(string type, CancellationToken cancellation);
    }
}
