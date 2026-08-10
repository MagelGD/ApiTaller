using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Workshop
{
    public class WorkshopOnboardingService : IWorkshopOnboardingService
    {
        private readonly DataContext _context;

        public WorkshopOnboardingService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> OnboardWorkshopAsync(WorkshopOnboardingRequestDto request)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                ApiTaller.Domain.Models.Workshop workshop = new ApiTaller.Domain.Models.Workshop
                {
                    Name = request.WorkshopName,
                    Slug = request.WorkshopName.ToLower().Replace(" ", "-"),
                    OwnerEmail = request.AdminEmail,
                    Phone = request.Phone,
                    Address = request.Address,
                    City = request.City,
                    WorkshopType = request.WorkshopType,
                    Plan = request.Plan,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Workshop.Add(workshop);
                await _context.SaveChangesAsync();

                UserRole adminRole = new UserRole
                {
                    Role = "Administrador",
                    WorkshopId = workshop.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.UserRole.Add(adminRole);
                await _context.SaveChangesAsync();

                string[] excludedModules = new[] { "Roles", "Configuracion Roles", "Modulos", "Operaciones", "Acciones", "Tipos Identificacion", "Modo Vehicular" };
                List<Module> activeModules = await _context.Module
                    .Where(m => m.IsActive && !excludedModules.Contains(m.Name))
                    .ToListAsync();
                    
                foreach (Module mod in activeModules)
                {
                    _context.UserRoleModule.Add(new UserRoleModule
                    {
                        UserRoleId = adminRole.Id,
                        ModulesRoleId = mod.Id,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                List<int> activeModuleIds = activeModules.Select(m => m.Id).ToList();
                string[] excludedActionSlugs = new[] { "Guardar_Usuarios" };
                List<Domain.Models.Action> activeActions = await _context.Action
                    .Where(a => a.IsActive && activeModuleIds.Contains(a.ModuleId) && !excludedActionSlugs.Contains(a.Slug))
                    .ToListAsync();
                    
                foreach (Domain.Models.Action act in activeActions)
                {
                    _context.RoleAction.Add(new RoleAction
                    {
                        RoleId = adminRole.Id,
                        ActionId = act.Id,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                await _context.SaveChangesAsync();

                User user = new User
                {
                    WorkshopId = workshop.Id,
                    UserRoleId = adminRole.Id,
                    IdentificationTypeId = request.IdentificationTypeId,
                    IdentificationNumber = request.AdminIdentification,
                    FirstName = request.AdminFirstName,
                    MiddleName = "",
                    FirstSurname = request.AdminFirstSurname,
                    SecondLastName = "",
                    FullName = $"{request.AdminFirstName} {request.AdminFirstSurname}".Trim(),
                    Username = request.AdminEmail,
                    Email = request.AdminEmail,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.AdminPassword),
                    IsActive = true,
                    MustChangePassword = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                await _context.Database.ExecuteSqlRawAsync("CALL sp_SeedWorkshopCatalogs({0})", workshop.Id);

                await transaction.CommitAsync();

                return workshop.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
