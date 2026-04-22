using ApiTaller.Domain.Dtos.RoleActions;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.RoleActions
{
    public interface IRoleActionsRepository
    {
        Task<List<string>> GetActionsByRoleIdAsync(int roleId, CancellationToken cancellationToken);
        Task<List<ValidateRolAction>> ValidateActionRoleAsync(int roleId, CancellationToken cancellationToken);
        Task<List<ActionsRole>> GetActionsByRoleAsync(int roleId, CancellationToken cancellationToken);
        Task<bool> SaveRoleAction(RoleAction roleAction, CancellationToken cancellationToken);
        Task<bool> ActiveOrInactiveRoleAction(RoleAction roleAction, CancellationToken cancellationToken);
        Task<bool> ValidateActionActive(int actionId, CancellationToken cancellationToken);
    }
}
