using ApiTaller.Domain.Dtos.Operation;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Operations
{
    public interface IOperationRepository
    {
        Task<IEnumerable<GetOperation>> GetOperations(CancellationToken cancellation = default!);
        Task<GetOperation?> GetOperationsById(int id, CancellationToken cancellation = default!);
        Task<GetOperation?> GetOperationName(string Operation, CancellationToken cancellation = default!);
        Task<bool> SaveOperation(Operation operation, CancellationToken cancellation = default!);
        Task<bool> UpdateOperation(Operation operation, CancellationToken cancellation = default!);

    }
}
