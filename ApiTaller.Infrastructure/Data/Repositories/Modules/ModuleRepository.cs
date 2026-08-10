using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


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
        public async Task<GetModuleDto?> GetModuleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetModuleDto? Query = await _context.Module.Where(x => x.Id == id).Select(x => new GetModuleDto
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

        public async Task<GetModuleDto?> GetModuleName(string Module, CancellationToken cancellation = default)
        {
            try
            {
                GetModuleDto? Query = await _context.Module.Where(x => x.Name == Module).Select(x => new GetModuleDto
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

        public async Task<IEnumerable<GetModuleDto>> GetModules(CancellationToken cancellation = default)
        {
            IEnumerable<GetModuleDto> Query = new List<GetModuleDto>();
            try
            {
                Query = await _context.Module.Select(x => new GetModuleDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                }).OrderBy(x=> x.Name).ToListAsync(cancellation);
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
                if (int.TryParse(_currentUser?.UserId, out int uid))
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
                if (int.TryParse(_currentUser?.UserId, out int uid))
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
