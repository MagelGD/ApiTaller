using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTaller.Domain.Dtos.Agenda
{
    /// <summary>
    /// DTO para que un CLIENTE NUEVO sin cuenta envíe una solicitud de cita (Flujo 2 - Público).
    /// </summary>
    public class PreRegisterAppointmentDto
    {
        [Required]
        public string ContactName { get; set; } = null!;

        [Required]
        public string ContactPhone { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        [Required]
        public string VehicleDescription { get; set; } = null!;

        public int? ServiceTypeId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        public string CustomerNotes { get; set; } = null!;
    }
}
