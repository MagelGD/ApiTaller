using System;

namespace ApiTaller.Domain.Dtos.Agenda
{
    public class AppointmentSummaryDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public string BookingSource { get; set; } = null!;
        public string ServiceTypeName { get; set; } = null!;

        // Para citas de clientes registrados
        public string CustomerName { get; set; } = null!;
        public string VehiclePlate { get; set; } = null!;

        // Para pre-registros y walk-ins nuevos
        public string ContactName { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string VehicleDescription { get; set; } = null!;
        public string CustomerNotes { get; set; } = null!;
        public int? WorkOrderId { get; set; }
        public string VehicleType { get; set; } = "moto";
    }
}
