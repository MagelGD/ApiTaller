using ApiTaller.Domain.Constants;
using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                // 1. Crear Taller (Tenant)
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

                // 2. Crear Rol Administrador para el Taller
                UserRole adminRole = new UserRole
                {
                    Role = "Administrador",
                    WorkshopId = workshop.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                _context.UserRole.Add(adminRole);
                await _context.SaveChangesAsync();

                // 3. Resolver los módulos seleccionados y validar dependencias
                List<Module> allActiveModules = await _context.Module
                    .Where(m => m.IsActive && !ModuleConstants.SuperAdminReservedModules.Contains(m.Name))
                    .ToListAsync();

                HashSet<int> resolvedModuleIds = new HashSet<int>();

                if (request.SelectedModuleIds != null && request.SelectedModuleIds.Any())
                {
                    // Filtrar solo los módulos activos que el SuperAdmin seleccionó
                    var requestedModules = allActiveModules.Where(m => request.SelectedModuleIds.Contains(m.Id)).ToList();
                    foreach (var m in requestedModules)
                    {
                        resolvedModuleIds.Add(m.Id);
                    }

                    // Resolver dependencias obligatorias del grafo
                    var nameToModule = allActiveModules.ToDictionary(m => m.Name, m => m, StringComparer.OrdinalIgnoreCase);

                    void EnsureModule(string moduleName)
                    {
                        if (nameToModule.TryGetValue(moduleName, out var mod))
                        {
                            resolvedModuleIds.Add(mod.Id);
                        }
                    }

                    // Reglas de dependencia entre módulos
                    bool hasWorkOrders = requestedModules.Any(m => m.Name.Equals(ModuleConstants.WorkOrders, StringComparison.OrdinalIgnoreCase));
                    bool hasPos = requestedModules.Any(m => m.Name.Equals(ModuleConstants.Pos, StringComparison.OrdinalIgnoreCase));
                    bool hasQuotations = requestedModules.Any(m => m.Name.Equals(ModuleConstants.Quotations, StringComparison.OrdinalIgnoreCase));
                    bool hasInventory = requestedModules.Any(m => m.Name.Equals(ModuleConstants.Inventory, StringComparison.OrdinalIgnoreCase));
                    bool hasAccounting = requestedModules.Any(m => m.Name.Equals(ModuleConstants.Accounting, StringComparison.OrdinalIgnoreCase));
                    bool hasAgenda = requestedModules.Any(m => m.Name.Equals(ModuleConstants.Agenda, StringComparison.OrdinalIgnoreCase));

                    if (hasWorkOrders)
                    {
                        EnsureModule(ModuleConstants.Customers);
                        EnsureModule(ModuleConstants.Vehicles);
                        EnsureModule(ModuleConstants.ServiceTypes);
                        EnsureModule(ModuleConstants.ServiceCatalogs);
                    }

                    if (hasPos)
                    {
                        EnsureModule(ModuleConstants.Products);
                        EnsureModule(ModuleConstants.Inventory);
                        EnsureModule(ModuleConstants.PaymentMethods);
                        EnsureModule(ModuleConstants.Customers);
                    }

                    if (hasQuotations)
                    {
                        EnsureModule(ModuleConstants.Customers);
                        EnsureModule(ModuleConstants.Vehicles);
                        EnsureModule(ModuleConstants.Products);
                        EnsureModule(ModuleConstants.ServiceCatalogs);
                    }

                    if (hasInventory)
                    {
                        EnsureModule(ModuleConstants.Products);
                        EnsureModule(ModuleConstants.ProductTypes);
                    }

                    if (hasAccounting)
                    {
                        EnsureModule(ModuleConstants.PaymentMethods);
                    }

                    if (hasAgenda)
                    {
                        EnsureModule(ModuleConstants.Customers);
                        EnsureModule(ModuleConstants.Vehicles);
                    }
                }
                else
                {
                    // Fallback: Si no se enviaron IDs específicos, asignar todos los módulos permitidos por defecto
                    foreach (var m in allActiveModules)
                    {
                        resolvedModuleIds.Add(m.Id);
                    }
                }

                List<Module> finalModules = allActiveModules.Where(m => resolvedModuleIds.Contains(m.Id)).ToList();

                // 4. Guardar en workshop_module (Feature Toggling por Taller)
                foreach (Module mod in finalModules)
                {
                    _context.WorkshopModule.Add(new WorkshopModule
                    {
                        WorkshopId = workshop.Id,
                        ModuleId = mod.Id,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                // 5. Asignar módulos al rol Administrador del Taller en UserRoleModule
                foreach (Module mod in finalModules)
                {
                    _context.UserRoleModule.Add(new UserRoleModule
                    {
                        UserRoleId = adminRole.Id,
                        ModulesRoleId = mod.Id,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    });
                }

                // 6. Asignar acciones de esos módulos al rol Administrador en RoleAction
                List<int> finalModuleIds = finalModules.Select(m => m.Id).ToList();
                string[] excludedActionSlugs = new[] { "Guardar_Usuarios" };
                List<Domain.Models.Action> activeActions = await _context.Action
                    .Where(a => a.IsActive && finalModuleIds.Contains(a.ModuleId) && !excludedActionSlugs.Contains(a.Slug))
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

                // 7. Crear el usuario Administrador del Taller
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

                // 8. Semillar catálogos base según opciones
                await _context.Database.ExecuteSqlRawAsync(
                    "CALL sp_SeedWorkshopCatalogs({0}, {1}, {2})",
                    workshop.Id,
                    request.SeedProducts ? 1 : 0,
                    request.SeedServices ? 1 : 0
                );

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
