using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace ApiTaller.Infrastructure.Data.Repositories.Modules
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly ILogger<ModuleRepository> _logger;
        private readonly DataContext _context;
        private readonly ICurrentUserService _currentUser;
        public ModuleRepository(ILogger<ModuleRepository> logger, DataContext dataContext, ICurrentUserService currentUser)
        {
            _context = dataContext;
            _logger = logger;
            _currentUser = currentUser;
        }
        public async Task<GetModule?> GetModuleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetModule? Query = await _context.Module.Where(x => x.Id == id).Select(x => new GetModule
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).FirstOrDefaultAsync(cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return default;
        }

        public async Task<GetModule?> GetModuleName(string Module, CancellationToken cancellation = default)
        {
            try
            {
                GetModule? Query = await _context.Module.Where(x => x.Name == Module).Select(x => new GetModule
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).FirstOrDefaultAsync(cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return null;
        }

        public async Task<IEnumerable<GetModule>> GetModules(CancellationToken cancellation = default)
        {
            IEnumerable<GetModule> Query = new List<GetModule>();
            try
            {
                Query = await _context.Module.Select(x => new GetModule
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return Query;
        }

        public async Task<bool> SaveModule(Module module, CancellationToken cancellation = default)
        {
            try
            {
                if (int.TryParse(_currentUser?.UserId, out var uid))
                {
                    module.ResponsibleUserId = uid;
                }
                module.CreatedAt = DateTime.UtcNow;
                await _context.Module.AddAsync(module, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }

        public async Task<bool> UpdateModule(Module module, CancellationToken cancellation = default)
        {
            try
            {
                if (int.TryParse(_currentUser?.UserId, out var uid))
                {
                    module.ResponsibleUserId = uid;
                }
                module.UpdatedAt = DateTime.UtcNow;
                _context.Module.Update(module);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }
    }
}
