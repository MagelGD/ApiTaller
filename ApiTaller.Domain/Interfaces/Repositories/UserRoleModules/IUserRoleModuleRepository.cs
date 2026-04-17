using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.UserRoleModules
{
    public interface IUserRoleModuleRepository
    {
        Task<IEnumerable<GetUserRoleModule>> GetUserRoleModules(CancellationToken cancellation = default!);
        Task<GetUserRoleModule?> GetUserRoleModuleById(int id, CancellationToken cancellation = default!);
        Task<bool> SaveUserRoleModule(UserRoleModule userRoleModule, CancellationToken cancellation = default!);
        Task<bool> UpdateUserRoleModule(UserRoleModule userRoleModule, CancellationToken cancellation = default!);
        Task<bool> ValidateExistUserRoleModule(int userRoleId, int moduleId, CancellationToken cancellation = default!);
        Task<GetUserRoleModule?> GetuserRoleModulesCrete(int userRoleId, int moduleId, CancellationToken cancellation = default!);
    }
}
