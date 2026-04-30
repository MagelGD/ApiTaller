using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class Customer : GeneralEntity
    {
        public int UserId { get; set; }
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual User UserIdNavigation { get; set; }
        public virtual IdentificationType IdentificationTypeNavigation { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
