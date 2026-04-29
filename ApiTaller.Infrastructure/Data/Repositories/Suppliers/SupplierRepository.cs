using ApiTaller.Domain.Dtos.Supplier;
using ApiTaller.Domain.Interfaces.Repositories.Suppliers;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ApiTaller.Infrastructure.Data.Repositories.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<SupplierRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public SupplierRepository(DataContext context, ILogger<SupplierRepository> logger, ICurrentUserService currentUserService)
        {
            _Context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }
        public Task<bool> CreateAsync(Supplier create, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetSupplierDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetSupplierDto> result = [];
            try
            {
                result = await _Context.Supplier
                    .Where(s => s.IsActive)
                    .Select(s => new GetSupplierDto
                    {
                        Id = s.Id,
                        DocumentNumber = s.DocumentNumber,
                        BusinessName = s.BusinessName,
                        ContactName = s.ContactName,
                        PhoneNumber = s.PhoneNumber,
                        Email = s.Email,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores activos");
            }
            return result;
        }

        public async Task<IEnumerable<GetSupplierDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetSupplierDto> result = [];
            try
            {
                result = await _Context.Supplier
                    .Select(s => new GetSupplierDto
                    {
                        Id = s.Id,
                        DocumentNumber = s.DocumentNumber,
                        BusinessName = s.BusinessName,
                        ContactName = s.ContactName,
                        PhoneNumber = s.PhoneNumber,
                        Email = s.Email,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores");
            }
            return result;
        }

        public async Task<GetSupplierDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetSupplierDto? result = null;
            try
            {
                result = await _Context.Supplier
                    .Where(s => s.Id == id)
                    .Select(s => new GetSupplierDto
                    {
                        Id = s.Id,
                        DocumentNumber = s.DocumentNumber,
                        BusinessName = s.BusinessName,
                        ContactName = s.ContactName,
                        PhoneNumber = s.PhoneNumber,
                        Email = s.Email,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el proveedor por id: {Id}", id);
            }
            return result;
        }

        public async Task<bool> UpdateAsync(Supplier update, CancellationToken cancellation)
        {
            try
            {
                if(int.TryParse(_currentUserService.UserId, out int userId))
                {
                    update.ResponsibleUserId = userId;
                }
                update.UpdatedAt = DateTime.Now;
                _Context.Supplier.Update(update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el proveedor con id: {Id}", update.Id);
            }
            return await _Context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<GetSupplierDto?> ValidateExist(GetSupplierDto type, CancellationToken cancellation)
        {
            GetSupplierDto? result = null;
            try
            {
                result = await _Context.Supplier
                    .Where(s => s.DocumentNumber.ToLower().Equals(type.DocumentNumber.ToLower()) || s.BusinessName.ToLower().Equals(type.BusinessName.ToLower()))
                    .Select(s => new GetSupplierDto
                    {
                        Id = s.Id,
                        DocumentNumber = s.DocumentNumber,
                        BusinessName = s.BusinessName,
                        ContactName = s.ContactName,
                        PhoneNumber = s.PhoneNumber,
                        Email = s.Email,
                        CreatedAt = s.CreatedAt,
                        UpdatedAt = s.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }

            return result;
        }
    }
}
