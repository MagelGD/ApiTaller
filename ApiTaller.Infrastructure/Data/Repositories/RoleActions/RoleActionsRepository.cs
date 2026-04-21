using ApiTaller.Domain.Dtos.RoleActions;
using ApiTaller.Domain.Interfaces.Repositories.RoleActions;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.RoleActions
{
    public class RoleActionsRepository : IRoleActionsRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<RoleActionsRepository> _logger;
        private readonly ICurrentUserService _currentUser;

        public RoleActionsRepository(DataContext context, ILogger<RoleActionsRepository> logger, ICurrentUserService currentUser)
        {
            _Context = context;
            _logger = logger;
            _currentUser = currentUser;
        }
        public async Task<bool> ActiveOrInactiveRoleAction(RoleAction roleAction, CancellationToken cancellationToken)
        {
            try
            {
                if(int.TryParse(_currentUser?.UserId, out int userId))
                {
                    roleAction.ResponsibleUserId = userId;
                }
                roleAction.UpdatedAt = DateTime.Now;
                _Context.Update(roleAction);
                return await _Context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ActiveOrInactiveRoleAction");
            }
            return false;
        }

        public async Task<List<ActionsRole>> GetActionsByRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            List<ActionsRole> actions = [];
            try
            {
                actions = await _Context.RoleAction.Include(x=> x.ActionIdNavigation).Where(ra => ra.RoleId == roleId)
                    .Select(ra => new ActionsRole
                    {
                        ActionId = ra.ActionId,
                        IsActive = ra.IsActive,
                        ModuleId = ra.ActionIdNavigation.ModuleId
                    })
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetActionsByRoleAsync");
            }
            return actions;
        }

        public async Task<List<string>> GetActionsByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            List<string> actions = [];
            try
            {
                actions = await _Context.RoleAction.Include(x => x.ActionIdNavigation)
                    .Where(ra => ra.RoleId == roleId && ra.IsActive)
                    .Select(ra => ra.ActionIdNavigation.Name)
                    .ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return actions;
        }

        public async Task<bool> SaveRoleAction(RoleAction roleAction, CancellationToken cancellationToken)
        {
            try
            {
                if(int.TryParse(_currentUser?.UserId, out int userId))
                {
                    roleAction.ResponsibleUserId = userId;
                }
                roleAction.CreatedAt = DateTime.Now;
                await _Context.RoleAction.AddAsync(roleAction, cancellationToken);
                return await _Context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SaveRoleAction");
            }
            return false;
        }

        public async Task<List<ValidateRolAction>> ValidateActionRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            List<ValidateRolAction> actions = [];
            try
            {
                actions = await _Context.RoleAction
                    .Where(ra => ra.RoleId == roleId)
                    .Select(ra => new ValidateRolAction
                    {
                        Id = ra.Id,
                        ActionId = ra.ActionId
                    }).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ValidateActionRoleAsync");
            }
            return actions;
        }
    }
}
