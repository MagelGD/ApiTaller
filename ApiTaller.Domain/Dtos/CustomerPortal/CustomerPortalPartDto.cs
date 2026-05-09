using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalPartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public bool IsProvidedByCustomer { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
    }
}
