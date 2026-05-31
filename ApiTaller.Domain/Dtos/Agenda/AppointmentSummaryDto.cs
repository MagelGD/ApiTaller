using System;

namespace ApiTaller.Domain.Dtos.Agenda
{
    public class AppointmentSummaryDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string BookingSource { get; set; }
        public string ServiceTypeName { get; set; }

        // Para citas de clientes registrados
        public string CustomerName { get; set; }
        public string VehiclePlate { get; set; }

        // Para pre-registros y walk-ins nuevos
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string VehicleDescription { get; set; }
        public string CustomerNotes { get; set; }
        public int? WorkOrderId { get; set; }
        public string VehicleType { get; set; } = "moto";
    }
}
