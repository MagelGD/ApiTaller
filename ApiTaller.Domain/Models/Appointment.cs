using System;

namespace ApiTaller.Domain.Models
{
    public class Appointment : GeneralEntity
    {
        // Cliente registrado (null para pre-registros)
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }

        // Tipo de servicio parametrizado (usa tabla service_type)
        public int? ServiceTypeId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string CustomerNotes { get; set; } = null!;

        // Estados: Pendiente | Agendada | Recibida | Cancelada | No Asistió | Rechazada
        public string Status { get; set; } = null!;

        // Origen: Portal | Pre-registro | Presencial
        public string BookingSource { get; set; } = null!;

        // Vínculo con Orden de Trabajo (se asigna al convertir)
        public int? WorkOrderId { get; set; }

        // Campos para clientes sin cuenta (Flujo 2 y Flujo 3 walk-in nuevo)
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public string? VehicleDescription { get; set; }
        /// <summary>SAAS-1: ID del taller al que pertenece esta cita</summary>
        public int WorkshopId { get; set; }

        // Navegación
        public virtual Customer CustomerNavigation { get; set; } = null!;
        public virtual Vehicle VehicleNavigation { get; set; } = null!;
        public virtual ServiceType ServiceTypeNavigation { get; set; } = null!;
        public virtual WorkOrder WorkOrderNavigation { get; set; } = null!;
    }
}
