using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Dtos.WorkOrder
{
    public class WorkOrderDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public int Mileage { get; set; }
        public string FuelLevel { get; set; } = null!;
        public string Observations { get; set; } = null!;
        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? ResponsibleUserId { get; set; }
        public string? CustomerName { get; set; }
        public string? VehiclePlate { get; set; }
        public string? VehicleBrand { get; set; }
        public string? VehicleVersion { get; set; }
        public string? VehicleType { get; set; } = "moto";
        public string? VehicleMotorization { get; set; }
        public bool IsBilled { get; set; }
        public decimal DownPayment { get; set; }

        public List<WorkOrderEvidenceDto>? Evidences { get; set; } 
        public List<WorkOrderPartDto>? Parts { get; set; }
        public List<WorkOrderServiceDto>? Services { get; set; } 
    }
}
