using ApiTaller.Domain.Dtos.UserRoleModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.UserRoleModules
{
    public interface IUserRoleModuleService
    {
        Task<IEnumerable<GetUserRoleModule>> GetUserRoleModules(CancellationToken cancellation = default!);
        Task<GetUserRoleModule?> GetUserRoleModuleById(int id, CancellationToken cancellation = default!);
        Task<GetUserRoleModule> SaveOrEditUserRoleModule(SaveUserRoleModule saveUserRoleModule, CancellationToken cancellation = default!);
    }
}
