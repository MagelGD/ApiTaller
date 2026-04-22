using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Repositories.RoleActions;
using ApiTaller.Domain.Interfaces.Services.Actions;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ApiTaller.Core.Services.Actions
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly ILogger<ActionService> _logger;
        private readonly IRoleActionsRepository _roleActionsRepository;

        public ActionService(IActionRepository actionRepository, ILogger<ActionService> logger, IRoleActionsRepository roleActionsRepository)
        {
            _actionRepository = actionRepository;
            _logger = logger;
            _roleActionsRepository = roleActionsRepository;
        }
        public async Task<IEnumerable<GetActions>> GetActions(CancellationToken cancellation = default)
        {
            IEnumerable<GetActions> actions = [];
            try
            {
                actions = await _actionRepository.GetActions(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las acciones");
            }
            return actions;
        }

        public async Task<IEnumerable<GetActions>> GetActionsActive(CancellationToken cancellation = default)
        {
            IEnumerable<GetActions> actions = [];
            try
            {
                actions = await _actionRepository.GetActionsActive(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las acciones");
            }
            return actions;
        }

        public async Task<GetActions?> GetActionsById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetActions? actions = await _actionRepository.GetActionsById(id, cancellation);
                return actions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando la accion por id");
            }
            return null;
        }

        public async Task<GetActions> SaveOrEditActions(GetActions action, CancellationToken cancellation = default)
        {
            GetActions data = new();
            try
            {
                Domain.Models.Action saveData = new()
                {
                    Id = action.Id,
                    ModuleId = action.Module.Id,
                    OperationId = action.Operation.Id,
                    Name = action.Name,
                    Slug = action.Name.Replace(" ", "_").ToLower(),
                    CreatedAt = DateTime.Now,
                    IsActive = action.IsActive
                };
                bool isExist = await ActionValidation(action, cancellation);
                if (saveData.Id == 0 && !isExist)
                {
                    await _actionRepository.SaveActions(saveData, cancellation);
                }
                else if (saveData.Id != 0) 
                {
                    bool isActive = await ActionActiveValidation(saveData.Id, cancellation);
                    if (saveData.IsActive)
                    {
                        await _actionRepository.UpdateActions(saveData, cancellation);
                    }
                    else if(!saveData.IsActive && !isActive)
                    {
                        await _actionRepository.UpdateActions(saveData, cancellation);
                    }
                }
                data = await _actionRepository.GetActionByName(saveData.Name, saveData.ModuleId, saveData.OperationId, cancellation) ?? new();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error guardando o editando la accion");
            }
            return data;
        }

        private async Task<bool> ActionValidation(GetActions action, CancellationToken cancellation = default)
        {
            try
            {
                return await _actionRepository.GetActionByExist(action.Name, action.Module.Id, action.Operation.Id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validando la accion");
            }
            return false;
        }

        private async Task<bool> ActionActiveValidation(int actionId, CancellationToken cancellation = default)
        {
            try
            {
                return await _roleActionsRepository.ValidateActionActive(actionId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validando si la accion esta activa en algun rol");
            }
            return false;
        }
    }
}
