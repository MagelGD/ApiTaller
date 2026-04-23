using ApiTaller.Domain.Dtos.RoleActions;
using ApiTaller.Domain.Interfaces.Repositories.RoleActions;
using ApiTaller.Domain.Interfaces.Services.RoleActions;
using Microsoft.Extensions.Logging;

namespace ApiTaller.Core.Services.RoleActions
{
    public class RoleActionService : IRoleActionService
    {
        private readonly IRoleActionsRepository _roleActionsRepository;
        private readonly ILogger<RoleActionService> _logger;

        public RoleActionService(IRoleActionsRepository roleActionsRepository, ILogger<RoleActionService> logger)
        {
            _roleActionsRepository = roleActionsRepository;
            _logger = logger;
        }

        public async Task<List<ActionsRoleDto>> GetActionsByRoleAsync(int roleId, CancellationToken cancellationToken)
        {
            List<ActionsRoleDto> actionsRoles = [];
            try
            {
                actionsRoles = await _roleActionsRepository.GetActionsByRoleAsync(roleId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return actionsRoles;
        }

        public async Task<List<string>> GetActionsByRoleIdAsync(int roleId, CancellationToken cancellationToken)
        {
            try
            {
                return await _roleActionsRepository.GetActionsByRoleIdAsync(roleId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultadon las acciones del rol");
            }
            return [];
        }
    }
}
