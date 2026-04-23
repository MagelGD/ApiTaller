using ApiTaller.Domain.Dtos.Operation;
using ApiTaller.Domain.Interfaces.Repositories.Operations;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.Operations
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<OperationRepository> _logger;
        private readonly ICurrentUserService _currentUser;
        public OperationRepository(DataContext context, ILogger<OperationRepository> logger, ICurrentUserService currentUser)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
        }
        public async Task<GetOperationDto?> GetOperationName(string Operation, CancellationToken cancellation = default)
        {
            try
            {
                GetOperationDto? operation = await _context.Operation
                    .Where(o => o.Name == Operation)
                    .Select(o => new GetOperationDto
                    {
                        Id = o.Id,
                        Name = o.Name,
                        IsActive = o.IsActive,
                        CreatedAt = o.CreatedAt,
                        UpdatedAt = o.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
                return operation;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el nombre de la operación");
            }
            return default;
        }

        public async Task<IEnumerable<GetOperationDto>> GetOperations(CancellationToken cancellation = default)
        {
            IEnumerable<GetOperationDto> operations = [];
            try
            {
                operations = await _context.Operation
                    .Select(o => new GetOperationDto
                    {
                        Id = o.Id,
                        Name = o.Name,
                        IsActive = o.IsActive,
                        CreatedAt = o.CreatedAt,
                        UpdatedAt = o.UpdatedAt
                    }).ToListAsync(cancellation);
                return operations;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las operaciones");
            }
            return operations;
        }

        public async Task<GetOperationDto?> GetOperationsById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetOperationDto? Query = await _context.Operation.Where(x => x.Id == id).Select(x => new GetOperationDto
                {
                    Id= x.Id,
                    Name= x.Name,
                    IsActive= x.IsActive,
                    CreatedAt= x.CreatedAt,
                    UpdatedAt= x.UpdatedAt
                }).FirstOrDefaultAsync(cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la operación por id");
            }
            return default;
        }

        public async Task<bool> SaveOperation(Operation operation, CancellationToken cancellation = default)
        {
            try
            {
                if(int.TryParse(_currentUser.UserId, out int userId))
                {
                    operation.ResponsibleUserId = userId;
                }
                operation.CreatedAt = DateTime.Now;
                await _context.Operation.AddAsync(operation, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la operación");
            }
            return false;
        }

        public async Task<bool> UpdateOperation(Operation operation, CancellationToken cancellation = default)
        {
            try
            {
                if (int.TryParse(_currentUser.UserId, out int userId))
                {
                    operation.ResponsibleUserId = userId;
                }
                operation.UpdatedAt = DateTime.Now;
                _context.Operation.Update(operation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la operacion");
            }
            return false;
        }
    }
}
