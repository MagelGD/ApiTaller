using ApiTaller.Domain.Dtos.Operation;
using ApiTaller.Domain.Interfaces.Repositories.Operations;
using ApiTaller.Domain.Interfaces.Services.Operations;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;

namespace ApiTaller.Core.Services.Operations
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _operationRepository;
        private readonly ILogger<OperationService> _logger;

        public OperationService(IOperationRepository operationRepository, ILogger<OperationService> logger)
        {
            _operationRepository = operationRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<GetOperationDto>> GetOperations(CancellationToken cancellation = default)
        {
            IEnumerable<GetOperationDto> operations = [];
            try
            {
                operations = await _operationRepository.GetOperations(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo traer todas las operaciones");
            }
            return operations;
        }

        public async Task<GetOperationDto?> GetOperationsById(int id, CancellationToken cancellation = default)
        {
            try
            {
                return await _operationRepository.GetOperationsById(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"No se pudo traer la operación con id {id}");
            }
            return null;
        }

        public async Task<GetOperationDto> SaveOrEditOperation(GetOperationDto operation, CancellationToken cancellation = default)
        {
            GetOperationDto data = new();
            try
            {
                Operation saveData = new()
                {
                    Id = operation.Id,
                    Name = operation.Name,
                    IsActive = operation.IsActive,
                    CreatedAt = operation.CreatedAt ?? new DateTime()
                };
                bool isExist = await OperationValidation(operation.Name, cancellation);
                if (saveData.Id == 0 && !isExist)
                {
                    _ = await _operationRepository.SaveOperation(saveData, cancellation);
                }
                else if (saveData.Id != 0)
                {
                    _ = await _operationRepository.UpdateOperation(saveData, cancellation);
                }
                data = await _operationRepository.GetOperationName(saveData.Name, cancellation) ?? new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"No se pudo guardar o editar la operación con id {operation.Id}");
            }
            return data;
        }

        private async Task<bool> OperationValidation(string name, CancellationToken cancellation = default)
        {
            try
            {
                GetOperationDto? operation = await _operationRepository.GetOperationName(name, cancellation);
                return operation != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"No se pudo validar la operación con nombre {name}");
            }
            return false;
        }
    }
}
