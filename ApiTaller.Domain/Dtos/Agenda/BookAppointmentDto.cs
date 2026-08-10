using System;

namespace ApiTaller.Domain.Dtos.Agenda
{
    /// <summary>
    /// DTO para que un CLIENTE REGISTRADO agende una cita desde su portal (Flujo 1).
    /// </summary>
    public class BookAppointmentDto
    {
        public int VehicleId { get; set; }
        public int? ServiceTypeId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string CustomerNotes { get; set; } = null!;
    }
}
