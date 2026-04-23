using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Interfaces.Repositories.UserRoleModules;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.UserRoleModules
{
    public class UserRoleModuleRepository : IUserRoleModuleRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<UserRoleModuleRepository> _logger;
        private readonly ICurrentUserService _currentUser;

        public UserRoleModuleRepository(DataContext context, ILogger<UserRoleModuleRepository> logger, ICurrentUserService currentUser)
        {
            _context = context;
            _logger = logger;
            _currentUser = currentUser;
        }
        public async Task<GetUserRoleModuleDto?> GetUserRoleModuleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                return await _context.UserRoleModule.Where(x => x.Id == id).Select(x => new GetUserRoleModuleDto
                {
                    id = x.Id,
                    Role = new GetUserRoleDto
                    {
                        IdUserRol = x.UserRoleIdNavigation.Id,
                        RoleName = x.UserRoleIdNavigation.Role,
                        IsActive = x.UserRoleIdNavigation.IsActive,
                        CreatedAt = x.UserRoleIdNavigation.CreatedAt,
                        UpdatedAt = x.UserRoleIdNavigation.UpdatedAt
                    },
                    Module = new GetModuleDto
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username
                }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el UserRoleModule con id {Id}", id);
            }
            return null;
        }

        public async Task<IEnumerable<GetUserRoleModuleDto>> GetUserRoleModules(CancellationToken cancellation = default)
        {
            IEnumerable<GetUserRoleModuleDto> userRoleModules = [];
            try
            {
                userRoleModules = await _context.UserRoleModule.Select(x => new GetUserRoleModuleDto
                {
                    id = x.Id,
                    Role = new GetUserRoleDto
                    {
                        IdUserRol = x.UserRoleIdNavigation.Id,
                        RoleName = x.UserRoleIdNavigation.Role,
                        IsActive = x.UserRoleIdNavigation.IsActive,
                        CreatedAt = x.UserRoleIdNavigation.CreatedAt,
                        UpdatedAt = x.UserRoleIdNavigation.UpdatedAt
                    },
                    Module = new GetModuleDto
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.CreatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username

                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los UserRoleModules");
            }
            return userRoleModules;
        }

        public async Task<GetUserRoleModuleDto?> GetuserRoleModulesCreate(int userRoleId, int moduleId, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRoleModuleDto? query = await _context.UserRoleModule.Where(x => x.UserRoleId == userRoleId && x.ModulesRoleId == moduleId).Select(x => new GetUserRoleModuleDto
                {
                    id = x.Id,
                    Role = new GetUserRoleDto
                    {
                        IdUserRol = x.UserRoleIdNavigation.Id,
                        RoleName = x.UserRoleIdNavigation.Role,
                        IsActive = x.UserRoleIdNavigation.IsActive,
                        CreatedAt = x.UserRoleIdNavigation.CreatedAt,
                        UpdatedAt = x.UserRoleIdNavigation.UpdatedAt
                    },
                    Module = new GetModuleDto
                    {
                        Id = x.ModuleIdNavigation.Id,
                        Name = x.ModuleIdNavigation.Name,
                        IsActive = x.ModuleIdNavigation.IsActive,
                        CreatedAt = x.ModuleIdNavigation.CreatedAt,
                        UpdatedAt = x.ModuleIdNavigation.UpdatedAt
                    },
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.CreatedAt,
                    ResponsibleUser = x.ResponsibleUserIdNavigation.Username
                }).FirstOrDefaultAsync(cancellation);
                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No fue posible realizar la consulta");
            }
            return null;
        }

        public async Task<bool> SaveUserRoleModule(UserRoleModule userRoleModule, CancellationToken cancellation = default)
        {
            try
            {
                if(int.TryParse(_currentUser?.UserId, out var uid))
                {
                    userRoleModule.ResponsibleUserId = uid;
                }
                userRoleModule.CreatedAt = DateTime.Now;
                await _context.UserRoleModule.AddAsync(userRoleModule, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo guardar el role con el modulo");
            }
            return false;
        }

        public async Task<bool> UpdateUserRoleModule(UserRoleModule userRoleModule, CancellationToken cancellation = default)
        {
            try
            {
                if(int.TryParse(_currentUser?.UserId, out var uid))
                {
                    userRoleModule.ResponsibleUserId = uid;
                }
                userRoleModule.UpdatedAt = DateTime.Now;
                _context.UserRoleModule.Update(userRoleModule);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol con el modulo");
            }
            return false;
        }

        public async Task<bool> ValidateExistUserRoleModule(int userRoleId, int moduleId, CancellationToken cancellation = default)
        {
            try
            {
                return await _context.UserRoleModule.AnyAsync(x => x.UserRoleId == userRoleId && x.ModulesRoleId == moduleId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar la existencia del rol con el modulo");
            }
            return false;
        }
    }
}
