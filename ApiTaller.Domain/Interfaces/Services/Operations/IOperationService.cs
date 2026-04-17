using ApiTaller.Domain.Dtos.Operation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Operations
{
    public interface IOperationService
    {
        Task<IEnumerable<GetOperation>> GetOperations(CancellationToken cancellation = default!);
        Task<GetOperation?> GetOperationsById(int id, CancellationToken cancellation = default!);
        Task<GetOperation> SaveOrEditOperation(GetOperation operation, CancellationToken cancellation = default!);
    }
}
