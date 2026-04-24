using ApiTaller.Domain.Dtos.Product;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Products
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetProductDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetProductDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<bool> CreateAsync(Product create, CancellationToken cancellation);
        Task<bool> UpdateAsync(Product update, CancellationToken cancellation);
        Task<GetProductDto?> ValidateExist(string name, int idProductType CancellationToken cancellation);
    }
}
