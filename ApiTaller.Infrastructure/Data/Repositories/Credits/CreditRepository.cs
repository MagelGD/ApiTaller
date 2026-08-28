using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Dtos.Credits;
using ApiTaller.Domain.Interfaces.Repositories.Credits;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Credits
{
    public class CreditRepository : ICreditRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CreditRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public CreditRepository(DataContext context, ILogger<CreditRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CustomerCreditSummaryDto>> GetCustomersWithCreditAsync(CancellationToken cancellation)
        {
            try
            {
                var salesWithBalance = await _context.Sale
                    .Include(s => s.Customer)
                    .Include(s => s.Payments)
                    .Where(s => s.IsActive && s.Balance > 0 && s.CustomerId > 0)
                    .ToListAsync(cancellation);

                var grouped = salesWithBalance
                    .GroupBy(s => s.CustomerId)
                    .Select(g =>
                    {
                        var firstSale = g.First();
                        var customer = firstSale.Customer;
                        var allPayments = g.SelectMany(s => s.Payments.Where(p => p.IsActive)).ToList();
                        DateTime? lastPaymentDate = allPayments.Any() 
                            ? allPayments.Max(p => p.PaymentDate ?? p.CreatedAt)
                            : null;

                        return new CustomerCreditSummaryDto
                        {
                            CustomerId = g.Key,
                            CustomerName = customer != null ? $"{customer.FirstName} {customer.LastName}".Trim() : $"Cliente #{g.Key}",
                            IdentificationNumber = customer?.IdentificationNumber,
                            PhoneNumber = customer?.PhoneNumber,
                            Email = customer?.Email,
                            TotalDebt = g.Sum(s => s.Balance),
                            PendingSalesCount = g.Count(),
                            LastSaleDate = g.Max(s => s.SaleDate),
                            LastPaymentDate = lastPaymentDate
                        };
                    })
                    .OrderByDescending(c => c.TotalDebt)
                    .ToList();

                return grouped;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving customers with credit");
                return Enumerable.Empty<CustomerCreditSummaryDto>();
            }
        }

        public async Task<CustomerCreditStatementDto?> GetCustomerStatementAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                var customer = await _context.Customer
                    .FirstOrDefaultAsync(c => c.Id == customerId && c.IsActive, cancellation);

                if (customer == null) return null;

                var sales = await _context.Sale
                    .Include(s => s.Payments)
                        .ThenInclude(p => p.PaymentMethod)
                    .Include(s => s.Details)
                    .Where(s => s.CustomerId == customerId && s.IsActive && s.Balance > 0)
                    .OrderByDescending(s => s.SaleDate)
                    .ToListAsync(cancellation);

                var pendingSales = sales.Select(s => new CreditSaleDto
                {
                    SaleId = s.Id,
                    SaleDate = s.SaleDate,
                    WorkOrderId = s.WorkOrderId,
                    Observations = s.Observations,
                    Subtotal = s.Subtotal,
                    DiscountAmount = s.DiscountAmount,
                    Total = s.Total,
                    DownPayment = s.DownPayment,
                    Balance = s.Balance,
                    Payments = s.Payments.Where(p => p.IsActive).Select(p => new SalePaymentDto
                    {
                        Id = p.Id,
                        PaymentMethodId = p.PaymentMethodId,
                        PaymentMethodName = p.PaymentMethod?.Name ?? "Efectivo",
                        Amount = p.Amount,
                        ReferenceCode = p.ReferenceCode,
                        PaymentDate = p.PaymentDate ?? p.CreatedAt,
                        Notes = p.Notes,
                        CreatedAt = p.CreatedAt
                    }).OrderBy(p => p.PaymentDate).ToList(),
                    Details = s.Details.Where(d => d.IsActive).Select(d => new SaleDetailDto
                    {
                        Id = d.Id,
                        ProductId = d.ProductId,
                        ServiceCatalogId = d.ServiceCatalogId,
                        Description = d.Description,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total
                    }).ToList()
                }).ToList();

                return new CustomerCreditStatementDto
                {
                    CustomerId = customer.Id,
                    CustomerName = $"{customer.FirstName} {customer.LastName}".Trim(),
                    IdentificationNumber = customer.IdentificationNumber,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Address = customer.Address,
                    TotalDebt = pendingSales.Sum(s => s.Balance),
                    PendingSales = pendingSales
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving statement for customer {customerId}");
                return null;
            }
        }

        public async Task<bool> RegisterPaymentAsync(RegisterCreditPaymentDto dto, CancellationToken cancellation)
        {
            try
            {
                if (dto.Amount <= 0)
                {
                    throw new ArgumentException("El monto del abono debe ser mayor a cero.");
                }

                var sale = await _context.Sale
                    .Include(s => s.Payments)
                    .FirstOrDefaultAsync(s => s.Id == dto.SaleId && s.IsActive, cancellation);

                if (sale == null)
                {
                    throw new InvalidOperationException($"La venta #{dto.SaleId} no existe o no está activa.");
                }

                if (sale.Balance <= 0)
                {
                    throw new InvalidOperationException($"La venta #{dto.SaleId} ya se encuentra totalmente saldada.");
                }

                int userId = int.TryParse(_currentUserService.UserId, out int uId) ? uId : 1;

                // Crear registro de abono/pago
                var payment = new SalePayment
                {
                    SaleId = sale.Id,
                    PaymentMethodId = dto.PaymentMethodId > 0 ? dto.PaymentMethodId : 1,
                    Amount = dto.Amount,
                    ReferenceCode = !string.IsNullOrWhiteSpace(dto.ReferenceCode) ? dto.ReferenceCode : "ABONO-CARTERA",
                    PaymentDate = dto.PaymentDate ?? DateTime.Now,
                    Notes = dto.Notes,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                };

                await _context.SalePayment.AddAsync(payment, cancellation);

                // Recalcular saldo de la venta
                sale.DownPayment += dto.Amount;
                sale.Balance = Math.Max(0, sale.Total - sale.DownPayment);
                sale.UpdatedAt = DateTime.Now;
                sale.ResponsibleUserId = userId;

                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error registering payment for sale {dto.SaleId}");
                throw;
            }
        }
    }
}
