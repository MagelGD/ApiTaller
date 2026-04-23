using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.UserRoles
{
    public interface IUserRoleService
    {
        Task<IEnumerable<GetUserRoleDto>> GetUserRoles(CancellationToken cancellation = default!);
        Task<GetUserRoleDto?> GetUserRoleById(int id, CancellationToken cancellation = default!);
        Task<GetUserRoleDto> SaveOrEditUserRole(GetUserRoleDto userRole, CancellationToken cancellation = default!);
    }
}
