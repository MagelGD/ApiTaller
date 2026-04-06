using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Services.UserRoles;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

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
        public async Task<GetUserRole?> GetUserRoleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRole? userRole = await _userRoleRepository.GetUserRoleById(id, cancellation);
                return userRole;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"");
            }
            return null;
        }

        public async Task<IEnumerable<GetUserRole>> GetUserRoles(CancellationToken cancellation = default)
        {
            IEnumerable<GetUserRole> userRoles = [];
            try
            {
                userRoles = await _userRoleRepository.GetUserRoles(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return userRoles?? [];
        }

        public async Task<GetUserRole> SaveOrEditUserRole(GetUserRole userRole, CancellationToken cancellation = default)
        {
            GetUserRole savedUserRole = new();
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
                if(data.Id == 0 && !exist)
                {
                    data.CreatedAt = DateTime.Now;
                    await _userRoleRepository.SaveUserRole(data, cancellation);
                }
                else
                {
                    data.UpdatedAt = DateTime.Now;
                    await _userRoleRepository.UpdateUserRole(data, cancellation);
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
                GetUserRole? userRole = await _userRoleRepository.GetUserRoleName(rolName, cancellation);
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
