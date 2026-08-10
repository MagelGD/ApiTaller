using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models
{
    public class Customer : GeneralEntity
    {
        public int UserId { get; set; }
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        /// <summary>SAAS-1: ID del taller al que pertenece este cliente</summary>
        public int WorkshopId { get; set; }

        public virtual User UserIdNavigation { get; set; } = null!;
        public virtual IdentificationType IdentificationTypeNavigation { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
