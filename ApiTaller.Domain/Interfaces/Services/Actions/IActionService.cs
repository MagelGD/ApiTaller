using ApiTaller.Domain.Dtos.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Actions
{
    public interface IActionService
    {
        Task<IEnumerable<GetActions>> GetActions(CancellationToken cancellation = default);
        Task<IEnumerable<GetActions>> GetActionsActive(CancellationToken cancellation = default);
        Task<GetActions?> GetActionsById(int id, CancellationToken cancellation = default);
        Task<GetActions> SaveOrEditActions(GetActions action, CancellationToken cancellation = default);
    }
}
