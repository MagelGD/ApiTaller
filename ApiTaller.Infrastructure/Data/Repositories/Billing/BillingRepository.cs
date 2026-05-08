using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Repositories.Billing;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Billing
{
    public class BillingRepository : IBillingRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;

        public BillingRepository(DataContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<bool> SaveSaleAsync(SaleDto saleDto, CancellationToken cancellation)
        {
            try
            {
                int.TryParse(_currentUserService.UserId, out int userId);
                int? finalUserId = userId != 0 ? userId : null;

                var sale = new Sale
                {
                    WorkOrderId = saleDto.WorkOrderId,
                    CustomerId = saleDto.CustomerId,
                    SaleDate = DateTime.Now,
                    Subtotal = saleDto.Subtotal,
                    DiscountPercent = saleDto.DiscountPercent,
                    DiscountAmount = saleDto.DiscountAmount,
                    Total = saleDto.Total,
                    DownPayment = saleDto.DownPayment,
                    Balance = saleDto.Balance,
                    Observations = saleDto.Observations,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = finalUserId
                };

                await _context.Sale.AddAsync(sale, cancellation);
                await _context.SaveChangesAsync(cancellation);

                // Guardar detalles
                if (saleDto.Details != null && saleDto.Details.Any())
                {
                    var details = saleDto.Details.Select(detailDto => new SaleDetail
                    {
                        SaleId = sale.Id,
                        ProductId = detailDto.ProductId,
                        ServiceCatalogId = detailDto.ServiceCatalogId,
                        Description = detailDto.Description,
                        Quantity = detailDto.Quantity,
                        UnitPrice = detailDto.UnitPrice,
                        Total = detailDto.Total,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = finalUserId
                    }).ToList();
                    
                    await _context.SaleDetail.AddRangeAsync(details, cancellation);
                }

                // Guardar pagos
                if (saleDto.Payments != null && saleDto.Payments.Any())
                {
                    var payments = saleDto.Payments.Select(paymentDto => new SalePayment
                    {
                        SaleId = sale.Id,
                        PaymentMethodId = paymentDto.PaymentMethodId,
                        Amount = paymentDto.Amount,
                        ReferenceCode = paymentDto.ReferenceCode,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = finalUserId
                    }).ToList();

                    await _context.SalePayment.AddRangeAsync(payments, cancellation);
                }

                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SaleDto> GetByWorkOrderAsync(int workOrderId, CancellationToken cancellation)
        {
            var sale = await _context.Sale
                .Include(s => s.Customer)
                .Include(s => s.Details)
                .Include(s => s.WorkOrder)
                    .ThenInclude(wo => wo.VehicleNavigation)
                        .ThenInclude(v => v.BrandNavigation)
                .Include(s => s.WorkOrder)
                    .ThenInclude(wo => wo.VehicleNavigation)
                        .ThenInclude(v => v.ModelNavigation)
                .Include(s => s.WorkOrder)
                    .ThenInclude(wo => wo.VehicleNavigation)
                        .ThenInclude(v => v.VersionNavigation)
                .FirstOrDefaultAsync(s => s.WorkOrderId == workOrderId, cancellation);

            if (sale == null) return null;

            var vehicle = sale.WorkOrder?.VehicleNavigation;
            var vehicleDisplay = vehicle != null
                ? $"{vehicle.BrandNavigation?.Name} {vehicle.ModelNavigation?.Models} {vehicle.VersionNavigation?.Version}".Trim()
                : "";

            return new SaleDto
            {
                Id = sale.Id,
                WorkOrderId = sale.WorkOrderId,
                CustomerId = sale.CustomerId,
                CustomerName = sale.Customer != null
                    ? $"{sale.Customer.FirstName} {sale.Customer.LastName}".Trim()
                    : "Consumidor Final",
                CustomerPhone = sale.Customer?.PhoneNumber,
                VehiclePlate = vehicle?.Plate,
                VehicleColor = vehicle?.Color,
                VehicleModel = vehicleDisplay,
                SaleDate = sale.SaleDate,
                Subtotal = sale.Subtotal,
                DiscountPercent = sale.DiscountPercent,
                DiscountAmount = sale.DiscountAmount,
                Total = sale.Total,
                DownPayment = sale.DownPayment,
                Balance = sale.Balance,
                Observations = sale.Observations,
                Details = sale.Details?.Select(d => new SaleDetailDto
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    ServiceCatalogId = d.ServiceCatalogId,
                    Description = d.Description,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    Total = d.Total
                }).ToList() ?? new List<SaleDetailDto>()
            };
        }
    }
}
