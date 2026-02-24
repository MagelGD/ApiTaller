using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class User : GeneralEntity
{
    public int RoleId { get; set; }
    public int IdentificationTypeId { get; set; }
    public string IdentificationNumber { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string FirstSurname { get; set; }
    public string SecondLastName { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime? AssignmentDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public virtual UserRole UserRoleIdNavigation { get; set; }
    public virtual IdentificationType IdentificationTypeIdNavigation { get; set; }









}
