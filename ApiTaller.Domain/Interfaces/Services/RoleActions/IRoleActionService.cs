using ApiTaller.Domain.Dtos.RoleActions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.RoleActions
{
    public interface IRoleActionService
    {
        Task<List<string>> GetActionsByRoleIdAsync(int roleId, CancellationToken cancellationToken);
        Task<List<ActionsRole>> GetActionsByRoleAsync(int roleId, CancellationToken cancellationToken);
    }
}
