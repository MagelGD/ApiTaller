using System;
using System.Collections.Generic;

namespace ApiTaller.Domain.Models;

public partial class User
{
    public int Id { get; set; }
    /// <summary>SAAS-1: ID del taller al que pertenece este usuario. NULL = Super Admin de la plataforma.</summary>
    public int? WorkshopId { get; set; }
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
    public string? Token { get; set; }
    public DateTime? AssignmentDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public bool MustChangePassword { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual UserRole UserRoleIdNavigation { get; set; } = null!;
    public virtual IdentificationType IdentificationTypeIdNavigation { get; set; } = null!;
    public virtual Workshop? WorkshopNavigation { get; set; }









}
