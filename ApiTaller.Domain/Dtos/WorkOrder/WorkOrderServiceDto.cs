using System;

namespace ApiTaller.Domain.Dtos.WorkOrder
{
    public class WorkOrderServiceDto
    {
        public int Id { get; set; }
        public int WorkOrderId { get; set; }
        public string Description { get; set; }
        public int MechanicId { get; set; }
        public decimal Price { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public bool IsActive { get; set; }
        public string? MechanicName { get; set; } 
        public bool IsApproved { get; set; }
    }
}
