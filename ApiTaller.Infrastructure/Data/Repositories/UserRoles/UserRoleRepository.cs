using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApiTaller.Infrastructure.Data.Repositories.UserRoles
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<UserRoleRepository> _logger;
        public UserRoleRepository(DataContext dataContext, ILogger<UserRoleRepository> logger)
        {
            _context = dataContext;
            _logger = logger;
        }
        public async Task<GetUserRole?> GetUserRoleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRole? Query = await (from ur in _context.UserRole
                                            where ur.Id == id
                                            select new GetUserRole
                                            {
                                                IdUserRol = ur.Id,
                                                RoleName = ur.Role,
                                                IsActive = ur.IsActive
                                            }).FirstOrDefaultAsync(cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return null;
        }

        public async Task<GetUserRole?> GetUserRoleName(string NameRol, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRole? Query = await (from ur in _context.UserRole
                                            where ur.Role == NameRol
                                            select new GetUserRole
                                            {
                                                IdUserRol = ur.Id,
                                                RoleName = ur.Role,
                                                IsActive = ur.IsActive
                                            }).FirstOrDefaultAsync(cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return null;
        }

        public async Task<IEnumerable<GetUserRole>> GetUserRoles(CancellationToken cancellation = default)
        {
            IEnumerable<GetUserRole> Query = [];
            try
            {
                Query = await (from ur in _context.UserRole
                               select new GetUserRole
                               {
                                   IdUserRol = ur.Id,
                                   RoleName = ur.Role,
                                   IsActive = ur.IsActive,
                                   CreatedAt = ur.CreatedAt,
                                   UpdatedAt = ur.UpdatedAt,
                               }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return Query ?? [];
        }

        public async Task<bool> SaveUserRole(UserRole userRole, CancellationToken cancellation = default)
        {
            try
            {
                await _context.UserRole.AddAsync(userRole, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return false;
        }

        public async Task<bool> UpdateUserRole(UserRole userRole, CancellationToken cancellation = default)
        {
            try
            {
                _context.Update(userRole);
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
