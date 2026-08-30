using ApiTaller.Domain.Dtos.Quotations;
using ApiTaller.Domain.Dtos.Billing;
using ApiTaller.Domain.Interfaces.Repositories.Quotations;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.Quotations
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUserService;

        public QuotationRepository(DataContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        private int? GetCurrentUserId()
        {
            return int.TryParse(_currentUserService.UserId, out int uid) && uid > 0 ? uid : null;
        }

        public async Task<IEnumerable<QuotationDto>> GetAllAsync(string? status, DateTime? startDate, DateTime? endDate, CancellationToken cancellation)
        {
            var query = _context.Quotation
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.BrandNavigation)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.ModelNavigation)
                .Include(q => q.Details)
                    .ThenInclude(d => d.Product)
                .Include(q => q.Details)
                    .ThenInclude(d => d.ServiceCatalog)
                .Include(q => q.Attachments)
                .Where(q => q.IsActive);

            if (!string.IsNullOrWhiteSpace(status) && !status.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(q => q.Status == status);
            }

            if (startDate.HasValue)
            {
                query = query.Where(q => q.CreatedAt >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                DateTime endOfDay = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(q => q.CreatedAt <= endOfDay);
            }

            var list = await query.OrderByDescending(q => q.CreatedAt).ToListAsync(cancellation);
            return list.Select(MapToDto);
        }

        public async Task<QuotationDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var entity = await _context.Quotation
                .AsNoTracking()
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.BrandNavigation)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.ModelNavigation)
                .Include(q => q.Details)
                    .ThenInclude(d => d.Product)
                .Include(q => q.Details)
                    .ThenInclude(d => d.ServiceCatalog)
                .Include(q => q.Attachments)
                .FirstOrDefaultAsync(q => q.Id == id && q.IsActive, cancellation);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<QuotationDto?> GetByTokenAsync(string token, CancellationToken cancellation)
        {
            var entity = await _context.Quotation
                .AsNoTracking()
                .IgnoreQueryFilters() // Para permitir acceso a clientes públicos por token
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.BrandNavigation)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.ModelNavigation)
                .Include(q => q.Details)
                    .ThenInclude(d => d.Product)
                .Include(q => q.Details)
                    .ThenInclude(d => d.ServiceCatalog)
                .Include(q => q.Attachments)
                .FirstOrDefaultAsync(q => q.PublicToken == token && q.IsActive, cancellation);

            return entity != null ? MapToDto(entity) : null;
        }

        public async Task<IEnumerable<QuotationDto>> GetByCustomerIdAsync(int customerId, CancellationToken cancellation)
        {
            var list = await _context.Quotation
                .AsNoTracking()
                .Include(q => q.Customer)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.BrandNavigation)
                .Include(q => q.Vehicle)
                    .ThenInclude(v => v.ModelNavigation)
                .Include(q => q.Details)
                    .ThenInclude(d => d.Product)
                .Include(q => q.Details)
                    .ThenInclude(d => d.ServiceCatalog)
                .Include(q => q.Attachments)
                .Where(q => q.CustomerId == customerId && q.IsActive)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync(cancellation);

            return list.Select(MapToDto);
        }

        public async Task<Quotation> CreateAsync(QuotationCreateDto dto, CancellationToken cancellation)
        {
            int? userId = GetCurrentUserId();
            int tenantId = _context.CurrentTenantId;

            // Generar número consecutivo seguro COT-0001
            int lastId = await _context.Quotation.IgnoreQueryFilters().Where(q => q.WorkshopId == tenantId).MaxAsync(q => (int?)q.Id, cancellation) ?? 0;
            int count = await _context.Quotation.IgnoreQueryFilters().CountAsync(q => q.WorkshopId == tenantId, cancellation) + 1;
            int nextNumber = Math.Max(count, lastId + 1);
            string quoteNumber = $"COT-{nextNumber:D4}";

            var quotation = new Quotation
            {
                QuotationNumber = quoteNumber,
                WorkshopId = tenantId,
                CustomerId = dto.CustomerId > 0 ? dto.CustomerId : null,
                VehicleId = dto.VehicleId > 0 ? dto.VehicleId : null,
                ProspectName = dto.ProspectName,
                ProspectEmail = dto.ProspectEmail,
                ProspectPhone = dto.ProspectPhone,
                ProspectVehicleInfo = dto.ProspectVehicleInfo,
                Status = "Draft",
                Subtotal = dto.Subtotal,
                DiscountPercent = dto.DiscountPercent,
                DiscountAmount = dto.DiscountAmount,
                Total = dto.Total,
                ExpirationDate = dto.ExpirationDate ?? DateTime.Now.AddDays(15),
                PublicToken = Guid.NewGuid().ToString("N"),
                Observations = dto.Observations,
                TermsAndConditions = dto.TermsAndConditions ?? "Cotización válida por 15 días. Sujeto a disponibilidad de inventario.",
                IsActive = true,
                CreatedAt = DateTime.Now,
                ResponsibleUserId = userId
            };

            if (dto.Details != null && dto.Details.Any())
            {
                foreach (var d in dto.Details)
                {
                    quotation.Details.Add(new QuotationDetail
                    {
                        ItemType = !string.IsNullOrWhiteSpace(d.ItemType) ? d.ItemType : "Product",
                        ProductId = (d.ProductId.HasValue && d.ProductId.Value > 0) ? d.ProductId.Value : null,
                        ServiceCatalogId = (d.ServiceCatalogId.HasValue && d.ServiceCatalogId.Value > 0) ? d.ServiceCatalogId.Value : null,
                        Description = !string.IsNullOrWhiteSpace(d.Description) ? d.Description : "Ítem de Cotización",
                        Quantity = d.Quantity > 0 ? d.Quantity : 1,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total > 0 ? d.Total : (d.Quantity * d.UnitPrice),
                        IsApproved = true,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    });
                }
            }

            if (dto.Attachments != null && dto.Attachments.Any())
            {
                foreach (var a in dto.Attachments)
                {
                    quotation.Attachments.Add(new QuotationAttachment
                    {
                        FileName = a.FileName,
                        ContentType = a.ContentType,
                        FileSizeBytes = a.FileSizeBytes,
                        Category = a.Category ?? "Photo",
                        DataBase64 = a.DataBase64,
                        FilePath = a.FilePath,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    });
                }
            }

            await _context.Quotation.AddAsync(quotation, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return quotation;
        }

        public async Task<bool> UpdateAsync(int id, QuotationCreateDto dto, CancellationToken cancellation)
        {
            var entity = await _context.Quotation
                .Include(q => q.Details)
                .Include(q => q.Attachments)
                .FirstOrDefaultAsync(q => q.Id == id && q.IsActive, cancellation);

            if (entity == null) return false;
            int? userId = GetCurrentUserId();

            entity.CustomerId = dto.CustomerId > 0 ? dto.CustomerId : null;
            entity.VehicleId = dto.VehicleId > 0 ? dto.VehicleId : null;
            entity.ProspectName = dto.ProspectName;
            entity.ProspectEmail = dto.ProspectEmail;
            entity.ProspectPhone = dto.ProspectPhone;
            entity.ProspectVehicleInfo = dto.ProspectVehicleInfo;
            entity.Subtotal = dto.Subtotal;
            entity.DiscountPercent = dto.DiscountPercent;
            entity.DiscountAmount = dto.DiscountAmount;
            entity.Total = dto.Total;
            entity.ExpirationDate = dto.ExpirationDate;
            entity.Observations = dto.Observations;
            entity.TermsAndConditions = dto.TermsAndConditions;
            entity.UpdatedAt = DateTime.Now;

            // Reemplazar detalles
            _context.QuotationDetail.RemoveRange(entity.Details);
            if (dto.Details != null && dto.Details.Any())
            {
                foreach (var d in dto.Details)
                {
                    entity.Details.Add(new QuotationDetail
                    {
                        QuotationId = entity.Id,
                        ItemType = !string.IsNullOrWhiteSpace(d.ItemType) ? d.ItemType : "Product",
                        ProductId = (d.ProductId.HasValue && d.ProductId.Value > 0) ? d.ProductId.Value : null,
                        ServiceCatalogId = (d.ServiceCatalogId.HasValue && d.ServiceCatalogId.Value > 0) ? d.ServiceCatalogId.Value : null,
                        Description = !string.IsNullOrWhiteSpace(d.Description) ? d.Description : "Ítem de Cotización",
                        Quantity = d.Quantity > 0 ? d.Quantity : 1,
                        UnitPrice = d.UnitPrice,
                        Total = d.Total > 0 ? d.Total : (d.Quantity * d.UnitPrice),
                        IsApproved = true,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    });
                }
            }

            // Actualizar adjuntos si vienen nuevos
            if (dto.Attachments != null && dto.Attachments.Any())
            {
                _context.QuotationAttachment.RemoveRange(entity.Attachments);
                foreach (var a in dto.Attachments)
                {
                    entity.Attachments.Add(new QuotationAttachment
                    {
                        QuotationId = entity.Id,
                        FileName = a.FileName,
                        ContentType = a.ContentType,
                        FileSizeBytes = a.FileSizeBytes,
                        Category = a.Category ?? "Photo",
                        DataBase64 = a.DataBase64,
                        FilePath = a.FilePath,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    });
                }
            }

            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<bool> UpdateStatusAsync(int id, string status, string? reason, CancellationToken cancellation)
        {
            var entity = await _context.Quotation.FirstOrDefaultAsync(q => q.Id == id && q.IsActive, cancellation);
            if (entity == null) return false;

            entity.Status = status;
            entity.UpdatedAt = DateTime.Now;
            if (status == "Sent") entity.SentAt = DateTime.Now;
            if (status == "Approved") entity.ApprovedAt = DateTime.Now;
            if (status == "Rejected")
            {
                entity.RejectedAt = DateTime.Now;
                entity.RejectionReason = reason;
            }

            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<bool> ProcessApprovalAsync(int id, QuotationApprovalRequestDto approvalDto, CancellationToken cancellation)
        {
            var entity = await _context.Quotation
                .Include(q => q.Details)
                .FirstOrDefaultAsync(q => q.Id == id && q.IsActive, cancellation);

            if (entity == null) return false;

            if (approvalDto.ApproveAll || approvalDto.ApprovedDetailIds == null || !approvalDto.ApprovedDetailIds.Any())
            {
                foreach (var detail in entity.Details)
                {
                    detail.IsApproved = true;
                }
                entity.Status = "Approved";
            }
            else
            {
                foreach (var detail in entity.Details)
                {
                    detail.IsApproved = approvalDto.ApprovedDetailIds.Contains(detail.Id);
                }

                bool anyApproved = entity.Details.Any(d => d.IsApproved);
                bool allApproved = entity.Details.All(d => d.IsApproved);

                entity.Status = allApproved ? "Approved" : (anyApproved ? "PartiallyApproved" : "Rejected");
            }

            // Recalcular total aprobado
            decimal approvedSubtotal = entity.Details.Where(d => d.IsApproved).Sum(d => d.Total);
            decimal discount = approvedSubtotal * (entity.DiscountPercent / 100m);
            entity.Subtotal = approvedSubtotal;
            entity.DiscountAmount = discount;
            entity.Total = approvedSubtotal - discount;
            entity.ApprovedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        public async Task<int> ConvertToWorkOrderAsync(QuotationConvertToOrderDto dto, CancellationToken cancellation)
        {
            var quote = await _context.Quotation
                .Include(q => q.Details)
                .FirstOrDefaultAsync(q => q.Id == dto.QuotationId && q.IsActive, cancellation);

            if (quote == null) throw new InvalidOperationException("Cotización no encontrada");

            int? userId = GetCurrentUserId();
            int tenantId = quote.WorkshopId;

            // Asegurar que exista cliente y vehiculo
            int customerId = quote.CustomerId ?? 0;
            int vehicleId = quote.VehicleId ?? 0;

            if (customerId == 0)
            {
                // Crear cliente rápido con datos del prospecto
                var newCustomer = new Customer
                {
                    FirstName = !string.IsNullOrWhiteSpace(quote.ProspectName) ? quote.ProspectName : "Cliente Cotización",
                    LastName = "General",
                    Email = quote.ProspectEmail,
                    PhoneNumber = quote.ProspectPhone,
                    Address = "N/A",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                };
                await _context.Customer.AddAsync(newCustomer, cancellation);
                await _context.SaveChangesAsync(cancellation);
                customerId = newCustomer.Id;
            }

            if (vehicleId == 0)
            {
                // Crear vehículo genérico para el cliente
                var newVehicle = new Vehicle
                {
                    CustomerId = customerId,
                    Plate = !string.IsNullOrWhiteSpace(quote.ProspectVehicleInfo) ? quote.ProspectVehicleInfo.Split(' ').FirstOrDefault() ?? "PENDIENTE" : "PENDIENTE",
                    Color = "Sin especificar",
                    VehicleType = "moto",
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                };
                await _context.Vehicle.AddAsync(newVehicle, cancellation);
                await _context.SaveChangesAsync(cancellation);
                vehicleId = newVehicle.Id;
            }

            var workOrder = new WorkOrder
            {
                CustomerId = customerId,
                VehicleId = vehicleId,
                EntryDate = dto.EntryDate ?? DateTime.Now,
                EstimatedDeliveryDate = dto.EstimatedDeliveryDate,
                Mileage = dto.Mileage,
                FuelLevel = dto.FuelLevel ?? "1/2",
                Observations = $"Generada automáticamente desde Cotización #{quote.QuotationNumber}. {quote.Observations}".Trim(),
                Status = "Aprobado",
                DownPayment = dto.DownPayment,
                WorkshopId = tenantId,
                IsActive = true,
                CreatedAt = DateTime.Now,
                ResponsibleUserId = userId
            };

            await _context.WorkOrder.AddAsync(workOrder, cancellation);
            await _context.SaveChangesAsync(cancellation);

            // Agregar repuestos aprobados
            var parts = quote.Details
                .Where(d => d.IsApproved && d.ItemType == "Product")
                .Select(d => new WorkOrderPart
                {
                    WorkOrderId = workOrder.Id,
                    ProductId = d.ProductId,
                    PartName = d.Description,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    IsApproved = true,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                }).ToList();

            if (parts.Any())
            {
                await _context.WorkOrderPart.AddRangeAsync(parts, cancellation);
            }

            // Agregar servicios aprobados
            var services = quote.Details
                .Where(d => d.IsApproved && d.ItemType == "Service")
                .Select(d => new WorkOrderService
                {
                    WorkOrderId = workOrder.Id,
                    Description = d.Description,
                    Price = d.UnitPrice,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                }).ToList();

            if (services.Any())
            {
                await _context.WorkOrderService.AddRangeAsync(services, cancellation);
            }

            // Marcar cotización como Convertida
            quote.Status = "Converted";
            quote.WorkOrderId = workOrder.Id;
            quote.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellation);
            return workOrder.Id;
        }

        public Task<int> ConvertToDirectSaleAsync(int quotationId, int paymentMethodId, string? referenceCode, CancellationToken cancellation)
        {
            var dto = new QuotationConvertToSaleDto
            {
                QuotationId = quotationId,
                Payments = new List<SalePaymentDto>
                {
                    new SalePaymentDto
                    {
                        PaymentMethodId = paymentMethodId > 0 ? paymentMethodId : 1,
                        ReferenceCode = referenceCode ?? "POS-DIRECT"
                    }
                }
            };
            return ConvertToDirectSaleDtoAsync(dto, cancellation);
        }

        public async Task<int> ConvertToDirectSaleDtoAsync(QuotationConvertToSaleDto dto, CancellationToken cancellation)
        {
            var quote = await _context.Quotation
                .Include(q => q.Details)
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(q => q.Id == dto.QuotationId && q.IsActive, cancellation);

            if (quote == null) throw new InvalidOperationException($"La cotización #{dto.QuotationId} no fue encontrada.");
            if (quote.Status == "Converted") throw new InvalidOperationException("Esta cotización ya fue convertida previamente.");

            int? userId = GetCurrentUserId();
            int tenantId = quote.WorkshopId;

            // Determinar o crear cliente
            int customerId = dto.CustomerId ?? quote.CustomerId ?? 0;
            if (customerId == 0)
            {
                var existingCust = await _context.Customer
                    .FirstOrDefaultAsync(c => c.IsActive && ((quote.ProspectEmail != null && c.Email == quote.ProspectEmail) || (quote.ProspectPhone != null && c.PhoneNumber == quote.ProspectPhone)), cancellation);

                if (existingCust != null)
                {
                    customerId = existingCust.Id;
                }
                else
                {
                    string pName = !string.IsNullOrWhiteSpace(quote.ProspectName) ? quote.ProspectName : "Cliente Mostrador";
                    var parts = pName.Split(' ', 2);
                    var newCust = new Customer
                    {
                        FirstName = parts[0],
                        LastName = parts.Length > 1 ? parts[1] : "",
                        PhoneNumber = quote.ProspectPhone ?? "N/A",
                        Email = quote.ProspectEmail ?? "cliente@pos.local",
                        Address = "N/A",
                        IdentificationNumber = "N/A",
                        IdentificationTypeId = 1,
                        WorkshopId = tenantId,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    };
                    await _context.Customer.AddAsync(newCust, cancellation);
                    await _context.SaveChangesAsync(cancellation);
                    customerId = newCust.Id;
                }
            }

            // Datos comerciales
            var settings = await _context.WorkshopSettings.Where(s => s.IsActive).ToListAsync(cancellation);
            string name = settings.FirstOrDefault(s => s.SettingKey == "workshop_name")?.SettingValue ?? "DAVID MOTOS";
            string slogan = settings.FirstOrDefault(s => s.SettingKey == "workshop_slogan")?.SettingValue ?? "SERVICIO TÉCNICO";
            string? logo = settings.FirstOrDefault(s => s.SettingKey == "logo")?.SettingValue;

            // Calcular montos de abono y saldo
            decimal totalPayments = dto.Payments != null && dto.Payments.Any() ? dto.Payments.Sum(p => p.Amount) : 0;
            decimal downPayment = dto.DownPayment > 0 ? dto.DownPayment : totalPayments;
            decimal balance = dto.Balance >= 0 && (dto.DownPayment > 0 || totalPayments > 0)
                ? dto.Balance
                : Math.Max(0, quote.Total - downPayment);

            var sale = new Sale
            {
                WorkOrderId = null,
                CustomerId = customerId,
                SaleDate = DateTime.Now,
                Subtotal = quote.Subtotal,
                DiscountPercent = quote.DiscountPercent,
                DiscountAmount = quote.DiscountAmount,
                Total = quote.Total,
                DownPayment = downPayment,
                Balance = balance,
                Observations = !string.IsNullOrWhiteSpace(dto.Observations) 
                    ? dto.Observations 
                    : $"Venta directa generada desde Cotización #{quote.QuotationNumber}. {quote.Observations}".Trim(),
                WorkshopName = name,
                WorkshopSlogan = slogan,
                LogoBase64 = logo,
                WorkshopId = tenantId,
                IsActive = true,
                CreatedAt = DateTime.Now,
                ResponsibleUserId = userId
            };

            await _context.Sale.AddAsync(sale, cancellation);
            await _context.SaveChangesAsync(cancellation);

            // Guardar detalles y descontar inventario
            var approvedDetails = quote.Details.Where(d => d.IsApproved).ToList();
            if (!approvedDetails.Any())
            {
                approvedDetails = quote.Details.Where(d => d.IsActive).ToList();
            }

            var saleDetails = approvedDetails.Select(d => new SaleDetail
            {
                SaleId = sale.Id,
                ProductId = d.ProductId,
                ServiceCatalogId = d.ServiceCatalogId,
                Description = d.Description,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Total = d.Total,
                IsActive = true,
                CreatedAt = DateTime.Now,
                ResponsibleUserId = userId
            }).ToList();

            await _context.SaleDetail.AddRangeAsync(saleDetails, cancellation);

            foreach (var d in saleDetails.Where(x => x.ProductId.HasValue && x.ProductId.Value > 0))
            {
                var prod = await _context.Product
                    .Include(p => p.ComboItems)
                        .ThenInclude(ci => ci.ChildProduct)
                    .FirstOrDefaultAsync(p => p.Id == d.ProductId!.Value, cancellation);

                if (prod != null && prod.IsCombo && prod.ComboItems.Any(ci => ci.IsActive))
                {
                    // Descontar componentes de combo
                    foreach (var ci in prod.ComboItems.Where(ci => ci.IsActive))
                    {
                        int qtyToDeduct = d.Quantity * ci.Quantity;
                        var compInv = await _context.Inventory.FirstOrDefaultAsync(i => i.ProductId == ci.ChildProductId, cancellation);
                        if (compInv == null)
                        {
                            compInv = new Domain.Models.Inventory
                            {
                                ProductId = ci.ChildProductId,
                                StockQuantity = -qtyToDeduct,
                                MinStock = 0,
                                CreatedAt = DateTime.Now,
                                IsActive = true,
                                ResponsibleUserId = userId
                            };
                            await _context.Inventory.AddAsync(compInv, cancellation);
                        }
                        else
                        {
                            compInv.StockQuantity -= qtyToDeduct;
                            compInv.LastUpdate = DateTime.Now;
                            compInv.UpdatedAt = DateTime.Now;
                        }

                        var compHistory = new InventoryHistory
                        {
                            ProductId = ci.ChildProductId,
                            MovementType = "Salida",
                            Quantity = qtyToDeduct,
                            ReferenceId = sale.Id,
                            Observations = $"Combo '{prod.ProductName}' en Cotización #{quote.QuotationNumber} (Venta #{sale.Id})",
                            CreatedAt = DateTime.Now,
                            IsActive = true,
                            ResponsibleUserId = userId
                        };
                        await _context.InventoryHistory.AddAsync(compHistory, cancellation);
                    }
                }
                else
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
                            ResponsibleUserId = userId
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
                        ReferenceId = sale.Id,
                        Observations = $"Venta de Cotización #{quote.QuotationNumber} (Venta #{sale.Id})",
                        CreatedAt = DateTime.Now,
                        IsActive = true,
                        ResponsibleUserId = userId
                    };
                    await _context.InventoryHistory.AddAsync(history, cancellation);
                }
            }

            // Registrar pagos
            if (dto.Payments != null && dto.Payments.Any())
            {
                foreach (var pay in dto.Payments.Where(p => p.Amount > 0))
                {
                    var payment = new SalePayment
                    {
                        SaleId = sale.Id,
                        PaymentMethodId = pay.PaymentMethodId > 0 ? pay.PaymentMethodId : 1,
                        Amount = pay.Amount,
                        ReferenceCode = !string.IsNullOrWhiteSpace(pay.ReferenceCode) ? pay.ReferenceCode : "POS-QUOTE",
                        PaymentDate = pay.PaymentDate ?? DateTime.Now,
                        Notes = pay.Notes,
                        IsActive = true,
                        CreatedAt = DateTime.Now,
                        ResponsibleUserId = userId
                    };
                    await _context.SalePayment.AddAsync(payment, cancellation);
                }
            }
            else if (downPayment > 0)
            {
                var payment = new SalePayment
                {
                    SaleId = sale.Id,
                    PaymentMethodId = 1,
                    Amount = downPayment,
                    ReferenceCode = "POS-QUOTE",
                    PaymentDate = DateTime.Now,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                };
                await _context.SalePayment.AddAsync(payment, cancellation);
            }

            // Actualizar cotización
            quote.Status = "Converted";
            quote.SaleId = sale.Id;
            quote.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellation);
            return sale.Id;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellation)
        {
            var entity = await _context.Quotation.FirstOrDefaultAsync(q => q.Id == id, cancellation);
            if (entity == null) return false;

            entity.IsActive = false;
            entity.UpdatedAt = DateTime.Now;
            return await _context.SaveChangesAsync(cancellation) > 0;
        }

        private static QuotationDto MapToDto(Quotation q)
        {
            string custName = q.Customer != null ? $"{q.Customer.FirstName} {q.Customer.LastName}".Trim() : null!;
            string vehPlate = q.Vehicle?.Plate;
            string vehModel = q.Vehicle != null ? $"{q.Vehicle.BrandNavigation?.Name} {q.Vehicle.ModelNavigation?.Models}".Trim() : null!;

            return new QuotationDto
            {
                Id = q.Id,
                QuotationNumber = q.QuotationNumber,
                WorkshopId = q.WorkshopId,
                CustomerId = q.CustomerId,
                CustomerName = custName,
                CustomerEmail = q.Customer?.Email,
                CustomerPhone = q.Customer?.PhoneNumber,
                VehicleId = q.VehicleId,
                VehiclePlate = vehPlate,
                VehicleModel = vehModel,
                ProspectName = q.ProspectName,
                ProspectEmail = q.ProspectEmail,
                ProspectPhone = q.ProspectPhone,
                ProspectVehicleInfo = q.ProspectVehicleInfo,
                Status = q.Status,
                Subtotal = q.Subtotal,
                DiscountPercent = q.DiscountPercent,
                DiscountAmount = q.DiscountAmount,
                Total = q.Total,
                CreatedAt = q.CreatedAt,
                ExpirationDate = q.ExpirationDate,
                PublicToken = q.PublicToken,
                Observations = q.Observations,
                TermsAndConditions = q.TermsAndConditions,
                WorkOrderId = q.WorkOrderId,
                SaleId = q.SaleId,
                SentAt = q.SentAt,
                ApprovedAt = q.ApprovedAt,
                RejectedAt = q.RejectedAt,
                RejectionReason = q.RejectionReason,
                HasServices = q.Details != null && q.Details.Any(d => d.ItemType == "Service"),
                HasProducts = q.Details != null && q.Details.Any(d => d.ItemType == "Product"),
                Details = q.Details != null ? q.Details.Where(d => d.IsActive).Select(d => new QuotationDetailDto
                {
                    Id = d.Id,
                    QuotationId = d.QuotationId,
                    ItemType = d.ItemType,
                    ProductId = d.ProductId,
                    ProductName = d.Product?.ProductName,
                    ServiceCatalogId = d.ServiceCatalogId,
                    ServiceCatalogName = d.ServiceCatalog?.Name,
                    Description = d.Description,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    Total = d.Total,
                    IsApproved = d.IsApproved
                }).ToList() : new List<QuotationDetailDto>(),
                Attachments = q.Attachments != null ? q.Attachments.Where(a => a.IsActive).Select(a => new QuotationAttachmentDto
                {
                    Id = a.Id,
                    QuotationId = a.QuotationId,
                    FileName = a.FileName,
                    ContentType = a.ContentType,
                    FileSizeBytes = a.FileSizeBytes,
                    Category = a.Category,
                    DataBase64 = a.DataBase64,
                    FilePath = a.FilePath
                }).ToList() : new List<QuotationAttachmentDto>()
            };
        }
    }
}
