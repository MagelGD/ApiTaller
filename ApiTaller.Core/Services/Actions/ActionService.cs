using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Services.Actions;
using Microsoft.Extensions.Logging;

namespace ApiTaller.Core.Services.Actions
{
    public class ActionService : IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly ILogger<ActionService> _logger;

        public ActionService(IActionRepository actionRepository, ILogger<ActionService> logger)
        {
            _actionRepository = actionRepository;
            _logger = logger;
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

        public Task<GetActions> SaveOrEditActions(GetActions action, CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }
    }
}
