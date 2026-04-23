using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Actions
{
    public interface IActionRepository
    {
        Task<IEnumerable<GetActionsDto>> GetActions(CancellationToken cancellation = default);
        Task<IEnumerable<GetActionsDto>> GetActionsActive(CancellationToken cancellation = default);
        Task<GetActionsDto?> GetActionsById(int id, CancellationToken cancellation = default);
        Task<bool> SaveActions(Models.Action action, CancellationToken cancellation = default);
        Task<bool> UpdateActions(Models.Action action, CancellationToken cancellation = default);
        Task<bool> GetActionByExist(string name, int idModule, int idOperation, CancellationToken cancellation = default);
        Task<GetActionsDto> GetActionByName(string name, int idModule, int idOperation, CancellationToken cancellation = default);

    }
}
