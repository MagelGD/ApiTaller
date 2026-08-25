using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.Quotations
{
    public class QuotationDetailDto
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public string ItemType { get; set; } = "Product"; // "Product" | "Service"
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? ServiceCatalogId { get; set; }
        public string? ServiceCatalogName { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public bool IsApproved { get; set; } = true;
    }

    public class QuotationAttachmentDto
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public string FileName { get; set; } = null!;
        public string? ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public string? Category { get; set; } // "Photo", "Diagnostic", "BudgetPDF"
        public string? DataBase64 { get; set; }
        public string? FilePath { get; set; }
    }

    public class QuotationDto
    {
        public int Id { get; set; }
        public string QuotationNumber { get; set; } = null!;
        public int WorkshopId { get; set; }
        
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        
        public int? VehicleId { get; set; }
        public string? VehiclePlate { get; set; }
        public string? VehicleModel { get; set; }
        
        public string? ProspectName { get; set; }
        public string? ProspectEmail { get; set; }
        public string? ProspectPhone { get; set; }
        public string? ProspectVehicleInfo { get; set; }

        public string ClientDisplayName => !string.IsNullOrWhiteSpace(CustomerName) 
            ? CustomerName 
            : (!string.IsNullOrWhiteSpace(ProspectName) ? ProspectName : "Prospecto / Sin Registrar");

        public string ClientDisplayEmail => !string.IsNullOrWhiteSpace(CustomerEmail) 
            ? CustomerEmail 
            : (!string.IsNullOrWhiteSpace(ProspectEmail) ? ProspectEmail : "");

        public string ClientDisplayPhone => !string.IsNullOrWhiteSpace(CustomerPhone) 
            ? CustomerPhone 
            : (!string.IsNullOrWhiteSpace(ProspectPhone) ? ProspectPhone : "");

        public string VehicleDisplayInfo => !string.IsNullOrWhiteSpace(VehiclePlate) 
            ? $"{VehiclePlate} ({VehicleModel})".Trim() 
            : (!string.IsNullOrWhiteSpace(ProspectVehicleInfo) ? ProspectVehicleInfo : "N/A");

        public string Status { get; set; } = "Draft";
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string PublicToken { get; set; } = null!;
        
        public string? Observations { get; set; }
        public string? TermsAndConditions { get; set; }
        
        public int? WorkOrderId { get; set; }
        public int? SaleId { get; set; }
        
        public DateTime? SentAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string? RejectionReason { get; set; }

        public bool HasServices { get; set; }
        public bool HasProducts { get; set; }

        public List<QuotationDetailDto> Details { get; set; } = new();
        public List<QuotationAttachmentDto> Attachments { get; set; } = new();
    }

    public class QuotationCreateDto
    {
        public int? Id { get; set; }
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }
        
        public string? ProspectName { get; set; }
        public string? ProspectEmail { get; set; }
        public string? ProspectPhone { get; set; }
        public string? ProspectVehicleInfo { get; set; }
        
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
        public string? Observations { get; set; }
        public string? TermsAndConditions { get; set; }
        
        public List<QuotationDetailDto> Details { get; set; } = new();
        public List<QuotationAttachmentDto> Attachments { get; set; } = new();
        
        public bool SendEmailImmediately { get; set; }
    }

    public class SendQuotationEmailDto
    {
        public int QuotationId { get; set; }
        public string ToEmail { get; set; } = null!;
        public string? CustomerName { get; set; }
        public string? PdfBase64 { get; set; }
        public string? CustomMessage { get; set; }
        public List<string>? AttachmentBase64s { get; set; }
    }

    public class QuotationApprovalRequestDto
    {
        public List<int>? ApprovedDetailIds { get; set; }
        public bool ApproveAll { get; set; } = true;
        public string? ClientNotes { get; set; }
        
        // Datos opcionales para agendamiento si aprueba servicios
        public DateTime? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
    }

    public class QuotationConvertToOrderDto
    {
        public int QuotationId { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public int Mileage { get; set; }
        public string FuelLevel { get; set; } = "1/2";
        public decimal DownPayment { get; set; }
    }
}
