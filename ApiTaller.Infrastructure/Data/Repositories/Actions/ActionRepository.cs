using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.Operation;
using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.Actions
{
    public class ActionRepository : IActionRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ActionRepository> _logger;
        private readonly ICurrentUserService _currentUser;
        public ActionRepository(DataContext context, ILogger<ActionRepository> logger, ICurrentUserService currentUser)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
        }

        public async Task<bool> GetActionByExist(string name, int idModule, int idOperation, CancellationToken cancellation = default)
        {
            try
            {
                bool Query = await _context.Action.Where(x => x.ModuleId == idModule && x.OperationId == idOperation && x.Name == name).AnyAsync(cancellationToken: cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la acción por slug");
            }
            return false;
        }

        public async Task<GetActions> GetActionByName(string name, int idModule, int idOperation, CancellationToken cancellation = default)
        {
            try
            {
                GetActions Query = await _context.Action.Where(x => x.ModuleId == idModule && x.OperationId == idOperation && x.Name == name).Select(x => new GetActions
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Module = new GetModule
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    Operation = new GetOperation
                    {
                        Id = x.OperationIdNavigation.Id,
                        Name = x.OperationIdNavigation.Name,
                        IsActive = x.OperationIdNavigation.IsActive,
                        CreatedAt = x.OperationIdNavigation.CreatedAt,
                        UpdatedAt = x.OperationIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username
                }).FirstOrDefaultAsync(cancellation) ?? new();
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la acción por slug");
            }
            return new();
        }

        public async Task<IEnumerable<GetActions>> GetActions(CancellationToken cancellation = default)
        {
            IEnumerable<GetActions> actions = [];
            try
            {
                actions = await _context.Action.Select(x => new GetActions
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Module = new GetModule
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    Operation = new GetOperation
                    {
                        Id = x.OperationIdNavigation.Id,
                        Name = x.OperationIdNavigation.Name,
                        IsActive = x.OperationIdNavigation.IsActive,
                        CreatedAt = x.OperationIdNavigation.CreatedAt,
                        UpdatedAt = x.OperationIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username
                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las acciones");
            }
            return actions;
        }

        public async Task<IEnumerable<GetActions>> GetActionsActive(CancellationToken cancellation = default)
        {
            IEnumerable<GetActions> actions = [];
            try
            {
                actions = await _context.Action.Where(x => x.IsActive).Select(x => new GetActions
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Module = new GetModule
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    Operation = new GetOperation
                    {
                        Id = x.OperationIdNavigation.Id,
                        Name = x.OperationIdNavigation.Name,
                        IsActive = x.OperationIdNavigation.IsActive,
                        CreatedAt = x.OperationIdNavigation.CreatedAt,
                        UpdatedAt = x.OperationIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username
                }).ToListAsync(cancellation);
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
                GetActions? action = _context.Action.Where(x => x.Id == id).Select(x => new GetActions
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Module = new GetModule
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    Operation = new GetOperation
                    {
                        Id = x.OperationIdNavigation.Id,
                        Name = x.OperationIdNavigation.Name,
                        IsActive = x.OperationIdNavigation.IsActive,
                        CreatedAt = x.OperationIdNavigation.CreatedAt,
                        UpdatedAt = x.OperationIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username
                }).FirstOrDefault();
                return action;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la acción por id");
            }
            return default;
        }

        public async Task<bool> SaveActions(Domain.Models.Action action, CancellationToken cancellation = default)
        {
            try
            {
                if (int.TryParse(_currentUser.UserId, out int userId))
                {
                    action.ResponsibleUserId = userId;
                }
                action.CreatedAt = DateTime.Now;
                await _context.Action.AddAsync(action, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la acción");
            }
            return false;
        }

        public async Task<bool> UpdateActions(Domain.Models.Action action, CancellationToken cancellation = default)
        {
            try
            {
                if (int.TryParse(_currentUser.UserId, out int userId))
                {
                    action.ResponsibleUserId = userId;
                }
                action.UpdatedAt = DateTime.Now;
                _context.Action.Update(action);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la acción");
            }
            return false;
        }
    }
}
