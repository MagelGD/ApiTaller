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

                // 1. Validar y resolver CustomerId desde la orden si viene en 0
                int finalCustomerId = saleDto.CustomerId;
                if (finalCustomerId == 0 && saleDto.WorkOrderId.HasValue)
                {
                    Domain.Models.WorkOrder? wo = await _context.WorkOrder.FindAsync(new object[] { saleDto.WorkOrderId.Value }, cancellation);
                    if (wo != null) finalCustomerId = wo.CustomerId;
                }

                // 2. Obtener datos comerciales del taller o aplicar fallbacks
                List<Domain.Models.WorkshopSettings> settings = await _context.WorkshopSettings
                    .Where(s => s.IsActive && (s.SettingKey == "logo" || s.SettingKey == "logo_brands" || s.SettingKey == "workshop_name" || s.SettingKey == "workshop_slogan"))
                    .ToListAsync(cancellation);
                string? logo = settings.FirstOrDefault(s => s.SettingKey == "logo")?.SettingValue;
                string? logoBrands = settings.FirstOrDefault(s => s.SettingKey == "logo_brands")?.SettingValue;
                string name = settings.FirstOrDefault(s => s.SettingKey == "workshop_name")?.SettingValue ?? "DAVID MOTOS";
                string slogan = settings.FirstOrDefault(s => s.SettingKey == "workshop_slogan")?.SettingValue ?? "SERVICIO TÉCNICO ESPECIALIZADO";

                Sale sale = new Sale
                {
                    WorkOrderId = saleDto.WorkOrderId,
                    CustomerId = finalCustomerId,
                    SaleDate = DateTime.Now,
                    Subtotal = saleDto.Subtotal,
                    DiscountPercent = saleDto.DiscountPercent,
                    DiscountAmount = saleDto.DiscountAmount,
                    Total = saleDto.Total,
                    DownPayment = saleDto.DownPayment,
                    Balance = saleDto.Balance,
                    Observations = saleDto.Observations,
                    WorkshopName = name,
                    WorkshopSlogan = slogan,
                    LogoBase64 = logo,
                    LogoBrandsBase64 = logoBrands,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = finalUserId
                };

                await _context.Sale.AddAsync(sale, cancellation);
                await _context.SaveChangesAsync(cancellation);

                // 3. Validar y guardar detalles
                if (saleDto.Details != null && saleDto.Details.Any())
                {
                    List<SaleDetail> details = new List<SaleDetail>();
                    foreach (var detailDto in saleDto.Details)
                    {
                        int? validProductId = (detailDto.ProductId.HasValue && detailDto.ProductId.Value > 0) ? detailDto.ProductId : null;
                        int? validServiceId = (detailDto.ServiceCatalogId.HasValue && detailDto.ServiceCatalogId.Value > 0) ? detailDto.ServiceCatalogId : null;

                        if (validProductId.HasValue)
                        {
                            var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == validProductId.Value && p.IsActive, cancellation);
                            if (product == null)
                            {
                                if (!saleDto.WorkOrderId.HasValue)
                                {
                                    throw new InvalidOperationException($"El producto solicitado no existe o está inactivo en el sistema.");
                                }
                                validProductId = null;
                            }
                            else if (!saleDto.WorkOrderId.HasValue) // Venta Directa (POS)
                            {
                                var currentInv = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == validProductId.Value, cancellation);
                                int availableStock = currentInv?.StockQuantity ?? 0;
                                int requestedQty = detailDto.Quantity > 0 ? detailDto.Quantity : 1;

                                if (availableStock < requestedQty)
                                {
                                    throw new InvalidOperationException($"Stock insuficiente para '{product.ProductName}'. Disponible: {availableStock}, Solicitado: {requestedQty}.");
                                }
                            }
                        }

                        if (validServiceId.HasValue)
                        {
                            bool exists = await _context.ServiceCatalog.AnyAsync(s => s.Id == validServiceId.Value, cancellation);
                            if (!exists) validServiceId = null;
                        }

                        details.Add(new SaleDetail
                        {
                            SaleId = sale.Id,
                            ProductId = validProductId,
                            ServiceCatalogId = validServiceId,
                            Description = !string.IsNullOrWhiteSpace(detailDto.Description) ? detailDto.Description : "Item Facturado",
                            Quantity = detailDto.Quantity > 0 ? detailDto.Quantity : 1,
                            UnitPrice = detailDto.UnitPrice,
                            Total = detailDto.Total > 0 ? detailDto.Total : (detailDto.Quantity * detailDto.UnitPrice),
                            IsActive = true,
                            CreatedAt = DateTime.Now,
                            ResponsibleUserId = finalUserId
                        });
                    }

                    await _context.SaleDetail.AddRangeAsync(details, cancellation);

                    // Descontar inventario automáticamente para los productos vendidos
                    foreach (var d in details.Where(x => x.ProductId.HasValue && x.ProductId.Value > 0))
                    {
                        var inv = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == d.ProductId!.Value, cancellation);
                        if (inv == null)
                        {
                            inv = new Domain.Models.Inventory
                            {
                                ProductId = d.ProductId!.Value,
                                StockQuantity = -d.Quantity,
                                MinStock = 0,
                                CreatedAt = DateTime.Now,
                                IsActive = true,
                                ResponsibleUserId = finalUserId
                            };
                            await _context.Inventory.AddAsync(inv, cancellation);
                        }
                        else
                        {
                            inv.StockQuantity -= d.Quantity;
                            inv.LastUpdate = DateTime.Now;
                            inv.UpdatedAt = DateTime.Now;
                        }

                        var history = new InventoryHistory
                        {
                            ProductId = d.ProductId!.Value,
                            MovementType = "Salida",
                            Quantity = d.Quantity,
                            ReferenceId = sale.WorkOrderId ?? sale.Id,
                            Observations = sale.WorkOrderId.HasValue 
                                ? $"Facturación OT #{sale.WorkOrderId.Value} (Venta #{sale.Id})"
                                : $"Venta Directa de Mostrador #{sale.Id}",
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            ResponsibleUserId = finalUserId
                        };
                        await _context.InventoryHistory.AddAsync(history, cancellation);
                    }
                }
                else if (!saleDto.WorkOrderId.HasValue)
                {
                    throw new InvalidOperationException("No se pueden registrar ventas de mostrador sin productos.");
                }

                // 4. Guardar pagos con ReferenceCode seguro
                if (saleDto.Payments != null && saleDto.Payments.Any())
                {
                    List<SalePayment> payments = new List<SalePayment>();
                    foreach (var paymentDto in saleDto.Payments)
                    {
                        int methodId = paymentDto.PaymentMethodId > 0 ? paymentDto.PaymentMethodId : 1;
                        bool methodExists = await _context.PaymentMethod.AnyAsync(p => p.Id == methodId, cancellation);
                        if (!methodExists)
                        {
                            var defaultMethod = await _context.PaymentMethod.FirstOrDefaultAsync(p => p.IsActive, cancellation);
                            if (defaultMethod != null) methodId = defaultMethod.Id;
                        }

                        payments.Add(new SalePayment
                        {
                            SaleId = sale.Id,
                            PaymentMethodId = methodId,
                            Amount = paymentDto.Amount,
                            ReferenceCode = !string.IsNullOrWhiteSpace(paymentDto.ReferenceCode) ? paymentDto.ReferenceCode : "N/A",
                            IsActive = true,
                            CreatedAt = DateTime.Now,
                            ResponsibleUserId = finalUserId
                        });
                    }

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
            Domain.Models.Sale? sale = await _context.Sale
                .Include(s => s.Customer)
                .Include(s => s.WorkOrder)
                    .ThenInclude(w => w.VehicleNavigation)
                        .ThenInclude(v => v.BrandNavigation)
                .Include(s => s.WorkOrder)
                    .ThenInclude(w => w.VehicleNavigation)
                        .ThenInclude(v => v.ModelNavigation)
                .Include(s => s.WorkOrder)
                    .ThenInclude(w => w.VehicleNavigation)
                        .ThenInclude(v => v.VersionNavigation)
                .Include(s => s.Details)
                    .ThenInclude(d => d.Product)
                .Include(s => s.Details)
                    .ThenInclude(d => d.Service)
                .Include(s => s.Payments)
                    .ThenInclude(p => p.PaymentMethod)
                .FirstOrDefaultAsync(s => s.WorkOrderId == workOrderId, cancellation);

            if (sale == null) return null!;

            Domain.Models.Vehicle? vehicle = sale.WorkOrder?.VehicleNavigation;
            string vehicleDisplay = vehicle != null
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
                CustomerEmail = sale.Customer?.Email,
                VehiclePlate = vehicle?.Plate,
                VehicleColor = vehicle?.Color,
                VehicleModel = vehicleDisplay,
                VehicleType = vehicle?.VehicleType ?? "moto",
                SaleDate = sale.SaleDate,
                Subtotal = sale.Subtotal,
                DiscountPercent = sale.DiscountPercent,
                DiscountAmount = sale.DiscountAmount,
                Total = sale.Total,
                DownPayment = sale.DownPayment,
                Balance = sale.Balance,
                Observations = sale.Observations,
                WorkshopName = sale.WorkshopName,
                WorkshopSlogan = sale.WorkshopSlogan,
                LogoBase64 = sale.LogoBase64,
                LogoBrandsBase64 = sale.LogoBrandsBase64,
                Details = sale.Details?.Select(d => new SaleDetailDto
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    ServiceCatalogId = d.ServiceCatalogId,
                    Description = d.Description,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    Total = d.Total
                }).ToList() ?? new List<SaleDetailDto>(),
                Payments = sale.Payments?.Select(p => new SalePaymentDto
                {
                    Id = p.Id,
                    PaymentMethodId = p.PaymentMethodId,
                    PaymentMethodName = p.PaymentMethod != null ? p.PaymentMethod.Name : "Efectivo",
                    Amount = p.Amount,
                    ReferenceCode = p.ReferenceCode
                }).ToList() ?? new List<SalePaymentDto>()
            };
        }
    }
}
