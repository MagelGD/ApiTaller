using ApiTaller.Domain.Dtos.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Actions
{
    public interface IActionService
    {
        Task<IEnumerable<GetActionsDto>> GetActions(CancellationToken cancellation = default);
        Task<IEnumerable<GetActionsDto>> GetActionsActive(CancellationToken cancellation = default);
        Task<GetActionsDto?> GetActionsById(int id, CancellationToken cancellation = default);
        Task<GetActionsDto> SaveOrEditActions(GetActionsDto action, CancellationToken cancellation = default);
    }
}
