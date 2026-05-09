using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalServiceDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
    }
}
