using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class Quotation : GeneralEntity
    {
        public string QuotationNumber { get; set; } = null!;
        public int WorkshopId { get; set; }
        
        // Cliente registrado (opcional)
        public int? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        
        // Vehículo registrado (opcional)
        public int? VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
        
        // Datos de prospecto cuando no es un cliente registrado
        public string? ProspectName { get; set; }
        public string? ProspectEmail { get; set; }
        public string? ProspectPhone { get; set; }
        public string? ProspectVehicleInfo { get; set; }
        
        // Estado: Draft, Sent, Approved, PartiallyApproved, Rejected, Expired, Converted
        public string Status { get; set; } = "Draft";
        
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
        
        public DateTime? ExpirationDate { get; set; }
        public string PublicToken { get; set; } = Guid.NewGuid().ToString("N");
        
        public string? Observations { get; set; }
        public string? TermsAndConditions { get; set; }
        
        // Si se convirtió a Orden de Trabajo o a Venta Directa
        public int? WorkOrderId { get; set; }
        public virtual WorkOrder? WorkOrder { get; set; }
        
        public int? SaleId { get; set; }
        public virtual Sale? Sale { get; set; }
        
        public DateTime? SentAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string? RejectionReason { get; set; }

        public virtual ICollection<QuotationDetail> Details { get; set; }
        public virtual ICollection<QuotationAttachment> Attachments { get; set; }

        public Quotation()
        {
            Details = new HashSet<QuotationDetail>();
            Attachments = new HashSet<QuotationAttachment>();
        }
    }
}
