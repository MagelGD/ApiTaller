using System;

namespace ApiTaller.Domain.Dtos.Portal
{
    public class PortalServiceDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
    }
}
