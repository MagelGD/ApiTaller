using ApiTaller.Domain.Dtos.UserRoleModule;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.UserRoleModules
{
    public interface IUserRoleModuleService
    {
        Task<IEnumerable<GetUserRoleModuleDto>> GetUserRoleModules(CancellationToken cancellation = default!);
        Task<GetUserRoleModuleDto?> GetUserRoleModuleById(int id, CancellationToken cancellation = default!);
        Task<GetUserRoleModuleDto> SaveOrEditUserRoleModule(SaveUserRoleModuleDto saveUserRoleModule, CancellationToken cancellation = default!);
    }
}
