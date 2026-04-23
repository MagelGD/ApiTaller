using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Models;

namespace ApiTaller.Domain.Interfaces.Repositories.UserRoles
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<GetUserRoleDto>> GetUserRoles(CancellationToken cancellation = default!);
        Task<GetUserRoleDto?> GetUserRoleById(int id, CancellationToken cancellation = default!);
        Task<GetUserRoleDto?> GetUserRoleName(string NameRol, CancellationToken cancellation = default!);
        Task<bool> SaveUserRole(UserRole userRole, CancellationToken cancellation = default!);
        Task<bool> UpdateUserRole(UserRole userRole, CancellationToken cancellation = default!);
    }
}

