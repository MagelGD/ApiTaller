using ApiTaller.Domain.Dtos.ProductType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.ProductTypes
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductTypeDto>> GetAllAsync(CancellationToken cancellation);
        Task<IEnumerable<GetProductTypeDto>> GetAllActiveAsync(CancellationToken cancellation);
        Task<GetProductTypeDto?> GetByIdAsync(int id, CancellationToken cancellation);
        Task<GetProductTypeDto> CreateOrEditProductType(GetProductTypeDto productType, CancellationToken cancellationToken);
    }
}
