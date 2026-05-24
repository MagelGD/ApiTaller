using ApiTaller.Domain.Dtos.Customer;
using ApiTaller.Domain.Interfaces.Repositories.Customers;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CustomerRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public CustomerRepository(DataContext context, ILogger<CustomerRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<bool> CreateAsync(Customer create, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_currentUserService.UserId, out int userId))
                {
                    create.ResponsibleUserId = userId;
                }
                create.CreatedAt = DateTime.Now;
                await _context.Customer.AddAsync(create, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
            }
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<IEnumerable<GetCustomerDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetCustomerDto> result = new List<GetCustomerDto>();
            try
            {
                result = await _context.Customer
                    .Where(c => c.IsActive)
                    .Select(c => new GetCustomerDto
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        IdentificationTypeId = c.IdentificationTypeId,
                        IdentificationNumber = c.IdentificationNumber,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active customers");
            }
            return result;
        }

        public async Task<IEnumerable<GetCustomerDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetCustomerDto> result = new List<GetCustomerDto>();
            try
            {
                result = await _context.Customer
                    .Select(c => new GetCustomerDto
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        IdentificationTypeId = c.IdentificationTypeId,
                        IdentificationNumber = c.IdentificationNumber,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all customers");
            }
            return result;
        }

        public async Task<GetCustomerDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetCustomerDto? result = null;
            try
            {
                result = await _context.Customer
                    .Where(c => c.Id == id)
                    .Select(c => new GetCustomerDto
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        IdentificationTypeId = c.IdentificationTypeId,
                        IdentificationNumber = c.IdentificationNumber,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting customer by id {id}");
            }
            return result;
        }

        public async Task<bool> UpdateAsync(Customer update, CancellationToken cancellation)
        {
            try
            {
                var existingCustomer = await _context.Customer.FindAsync(new object[] { update.Id }, cancellation);
                if (existingCustomer == null) return false;

                if (int.TryParse(_currentUserService.UserId, out int userId))
                    update.ResponsibleUserId = userId;

                update.UpdatedAt = DateTime.Now;

                _context.Entry(existingCustomer).CurrentValues.SetValues(update);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el cliente {Id}", update.Id);
                throw;
            }
        }


        public async Task<GetCustomerDto?> ValidateExist(GetCustomerDto data, CancellationToken cancellation)
        {
            GetCustomerDto? result = null;
            try
            {
                result = await _context.Customer
                    .Where(c => c.IdentificationNumber.ToLower() == data.IdentificationNumber.ToLower())
                    .Select(c => new GetCustomerDto
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        IdentificationTypeId = c.IdentificationTypeId,
                        IdentificationNumber = c.IdentificationNumber,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        PhoneNumber = c.PhoneNumber,
                        Email = c.Email,
                        Address = c.Address,
                        IsActive = c.IsActive,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    })
                    .FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating customer existence");
            }
            return result;
        }

        public async Task<(bool HasActive, int WorkOrderId, string? Plate, string Status)?> GetActiveWorkOrderInfoAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                var wo = await _context.WorkOrder
                    .Include(w => w.VehicleNavigation)
                    .Where(w => w.CustomerId == customerId && w.IsActive &&
                                (w.Status != "Entregado" || !_context.Sale.Any(s => s.WorkOrderId == w.Id && s.IsActive)))
                    .FirstOrDefaultAsync(cancellation);

                if (wo == null) return null;
                return (true, wo.Id, wo.VehicleNavigation?.Plate?.ToUpper(), wo.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar órdenes activas del cliente {Id}", customerId);
                return null;
            }
        }
    }
}

