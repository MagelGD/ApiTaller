using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Actions
{
    public interface IActionRepository
    {
        Task<IEnumerable<GetActions>> GetActions(CancellationToken cancellation = default);
        Task<GetActions?> GetActionsById(int id, CancellationToken cancellation = default);
        Task<bool> SaveActions(Models.Action action, CancellationToken cancellation = default);
        Task<bool> UpdateActions(Models.Action action, CancellationToken cancellation = default);
        Task<bool> GetActionByExist(string name, int idModule, int idOperation, CancellationToken cancellation = default);
        Task<GetActions> GetActionByName(string name, int idModule, int idOperation, CancellationToken cancellation = default);

    }
}
