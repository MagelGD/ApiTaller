using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalAppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public string VehicleDescription { get; set; } = null!;
        public string ServiceTypeName { get; set; } = null!;
        public string CustomerNotes { get; set; } = null!;
    }
}
