using ApiTaller.Domain.Dtos.Operation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Operations
{
    public interface IOperationService
    {
        Task<IEnumerable<GetOperationDto>> GetOperations(CancellationToken cancellation = default!);
        Task<GetOperationDto?> GetOperationsById(int id, CancellationToken cancellation = default!);
        Task<GetOperationDto> SaveOrEditOperation(GetOperationDto operation, CancellationToken cancellation = default!);
    }
}
