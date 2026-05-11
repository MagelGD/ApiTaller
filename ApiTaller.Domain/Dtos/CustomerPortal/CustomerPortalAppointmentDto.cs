using System;

namespace ApiTaller.Domain.Dtos.CustomerPortal
{
    public class CustomerPortalAppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string VehicleDescription { get; set; }
        public string ServiceTypeName { get; set; }
        public string CustomerNotes { get; set; }
    }
}
