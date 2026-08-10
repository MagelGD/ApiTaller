using System;
using System.Collections.Generic;
using System.Text;
using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Dtos.UserRole;

namespace ApiTaller.Domain.Dtos.Users
{
    public class GetUsersDto
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public int IdentificationTypeId { get; set; }
        public string IdentificationNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string FirstSurname { get; set; } = null!;
        public string SecondLastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? AssignmentDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public GetUserRoleDto? UserRoleDto { get; set; }
        public GetIdentificationTypeDto? IdentificationTypeDto { get; set; }
    }
}
