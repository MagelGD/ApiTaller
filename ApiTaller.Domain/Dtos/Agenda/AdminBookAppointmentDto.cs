using System;
using System.ComponentModel.DataAnnotations;

namespace ApiTaller.Domain.Dtos.Agenda
{
    /// <summary>
    /// DTO para que el ADMIN agende manualmente una cita (Flujo 3 - Presencial).
    /// Soporta cliente existente (CustomerId + VehicleId) o cliente nuevo (campos de contacto).
    /// </summary>
    public class AdminBookAppointmentDto
    {
        // Opción A: cliente existente
        public int? CustomerId { get; set; }
        public int? VehicleId { get; set; }

        // Opción B: cliente nuevo walk-in
        public string ContactName { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string VehicleDescription { get; set; } = null!;

        public int? ServiceTypeId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        public string CustomerNotes { get; set; } = null!;

        /// <summary>
        /// Si true, fuerza el agendamiento aunque el día esté lleno.
        /// </summary>
        public bool ForceBook { get; set; } = false;
    }
}
