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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Crear el Taller
                var workshop = new ApiTaller.Domain.Models.Workshop
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

                // 2. Crear Rol Administrador Local
                var adminRole = new UserRole
                {
                    Role = "Administrador",
                    WorkshopId = workshop.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.UserRole.Add(adminRole);
                await _context.SaveChangesAsync();

                // 3. Asignar todos los modulos activos al nuevo rol administrador
                var activeModules = await _context.Module.Where(m => m.IsActive).ToListAsync();
                foreach (var mod in activeModules)
                {
                    _context.UserRoleModule.Add(new UserRoleModule
                    {
                        UserRoleId = adminRole.Id,
                        ModulesRoleId = mod.Id,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                // Asignar todas las acciones activas al nuevo rol administrador
                var activeActions = await _context.Action.Where(a => a.IsActive).ToListAsync();
                foreach (var act in activeActions)
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

                // 4. Crear Usuario Dueño
                var user = new User
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
                    Username = request.AdminEmail, // Usamos el correo como username
                    Email = request.AdminEmail,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.AdminPassword),
                    IsActive = true,
                    MustChangePassword = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                // 5. Poblar Catálogo (Clonar del Template Workshop)
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
