using System;

namespace ApiTaller.Domain.Models
{
    public class QuotationAttachment : GeneralEntity
    {
        public int QuotationId { get; set; }
        public virtual Quotation Quotation { get; set; } = null!;

        public string FileName { get; set; } = null!;
        public string? ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public string? Category { get; set; } // "Photo", "Diagnostic", "BudgetPDF"
        public string? DataBase64 { get; set; }
        public string? FilePath { get; set; }
    }
}
