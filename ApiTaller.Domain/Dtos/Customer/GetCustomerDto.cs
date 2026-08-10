using System;

namespace ApiTaller.Domain.Dtos.Customer
{
    public class GetCustomerDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
