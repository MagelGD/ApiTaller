using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.RoleActions
{
    public interface IRoleActionsRepository
    {
        Task<List<string>> GetActionsByRoleIdAsync(int roleId, CancellationToken cancellationToken);
        Task<RoleAction> SaveRoleAction(RoleAction roleAction, CancellationToken cancellationToken);
        Task<RoleAction> ActiveOrInactiveRoleAction(RoleAction roleAction, CancellationToken cancellationToken);
    }
}
