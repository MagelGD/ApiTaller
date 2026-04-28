using ApiTaller.Domain.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetProductDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetProductDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetProductDto> CreateOrEditProductType(GetProductDto product, CancellationToken cancellationToken);
    }
}
