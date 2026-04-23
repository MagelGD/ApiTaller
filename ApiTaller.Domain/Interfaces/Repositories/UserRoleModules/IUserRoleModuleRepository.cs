using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.UserRoleModules
{
    public interface IUserRoleModuleRepository
    {
        Task<IEnumerable<GetUserRoleModuleDto>> GetUserRoleModules(CancellationToken cancellation = default!);
        Task<GetUserRoleModuleDto?> GetUserRoleModuleById(int id, CancellationToken cancellation = default!);
        Task<bool> SaveUserRoleModule(UserRoleModule userRoleModule, CancellationToken cancellation = default!);
        Task<bool> UpdateUserRoleModule(UserRoleModule userRoleModule, CancellationToken cancellation = default!);
        Task<bool> ValidateExistUserRoleModule(int userRoleId, int moduleId, CancellationToken cancellation = default!);
        Task<GetUserRoleModuleDto?> GetuserRoleModulesCreate(int userRoleId, int moduleId, CancellationToken cancellation = default!);
    }
}
