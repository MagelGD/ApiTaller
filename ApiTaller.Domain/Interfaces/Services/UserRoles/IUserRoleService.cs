using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.UserRoles
{
    public interface IUserRoleService
    {
        Task<IEnumerable<GetUserRole>> GetUserRoles(CancellationToken cancellation = default!);
        Task<GetUserRole?> GetUserRoleById(int id, CancellationToken cancellation = default!);
        Task<GetUserRole> SaveOrEditUserRole(GetUserRole userRole, CancellationToken cancellation = default!);
    }
}
