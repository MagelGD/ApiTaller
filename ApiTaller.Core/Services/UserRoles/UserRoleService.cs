using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Services.UserRoles;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ApiTaller.Core.Services.UserRoles
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ILogger<UserRoleService> _logger;
        public UserRoleService(IUserRoleRepository userRoleRepository, ILogger<UserRoleService> logger)
        {
            _logger = logger;
            _userRoleRepository = userRoleRepository;
        }
        public async Task<GetUserRoleDto?> GetUserRoleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRoleDto? userRole = await _userRoleRepository.GetUserRoleById(id, cancellation);
                return userRole;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return null;
        }

        public async Task<IEnumerable<GetUserRoleDto>> GetUserRoles(CancellationToken cancellation = default)
        {
            IEnumerable<GetUserRoleDto> userRoles = [];
            try
            {
                userRoles = await _userRoleRepository.GetUserRoles(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return userRoles ?? [];
        }

        public async Task<GetUserRoleDto> SaveOrEditUserRole(GetUserRoleDto userRole, CancellationToken cancellation = default)
        {
            GetUserRoleDto savedUserRole = new();
            try
            {
                UserRole data = new()
                {
                    Id = userRole.IdUserRol,
                    Role = userRole.RoleName,
                    IsActive = userRole.IsActive,
                    CreatedAt = userRole.CreatedAt ?? new DateTime()
                };
                bool exist = await RolValidation(userRole.RoleName, cancellation);
                if (data.Id == 0 && !exist)
                {
                    data.CreatedAt = DateTime.Now;
                    _ = await _userRoleRepository.SaveUserRole(data, cancellation);
                }
                else
                {
                    data.UpdatedAt = DateTime.Now;
                    _ = await _userRoleRepository.UpdateUserRole(data, cancellation);
                }
                savedUserRole = await _userRoleRepository.GetUserRoleName(data.Role, cancellation) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return savedUserRole;
        }

        private async Task<bool> RolValidation(string rolName, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRoleDto? userRole = await _userRoleRepository.GetUserRoleName(rolName, cancellation);
                return userRole != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }
    }
}
