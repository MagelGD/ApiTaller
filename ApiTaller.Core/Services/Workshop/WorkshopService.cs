using ApiTaller.Domain.Constants;
using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Repositories.Workshop;
using ApiTaller.Domain.Interfaces.Services.Workshop;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Workshop
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly DataContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<WorkshopService> _logger;

        public WorkshopService(
            IWorkshopRepository workshopRepository,
            DataContext context,
            IMemoryCache memoryCache,
            ILogger<WorkshopService> logger)
        {
            _workshopRepository = workshopRepository;
            _context = context;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<RegisterWorkshopResponseDto> RegisterWorkshopAsync(RegisterWorkshopDto dto, CancellationToken ct = default)
        {
            try
            {
                // 1. Validar el tipo de negocio
                if (dto.WorkshopType != "moto" && dto.WorkshopType != "car" && dto.WorkshopType != "multi" && dto.WorkshopType != "oil_change")
                {
                    return new RegisterWorkshopResponseDto { Success = false, Message = "Tipo de taller inválido. Valores permitidos: moto, car, multi, oil_change." };
                }

                // 2. Generar Slug único
                string slug = GenerateSlug(dto.Name);
                bool exists = await _workshopRepository.SlugExistsAsync(slug, ct);
                if (exists)
                {
                    slug = $"{slug}-{DateTime.Now.Ticks.ToString().Substring(10)}";
                }

                // 3. Crear Entidad
                Domain.Models.Workshop workshop = new Domain.Models.Workshop
                {
                    Name = dto.Name,
                    Slug = slug,
                    OwnerEmail = dto.OwnerEmail,
                    Phone = dto.Phone,
                    Address = dto.Address,
                    City = dto.City,
                    WorkshopType = dto.WorkshopType,
                    Plan = dto.Plan,
                    IsActive = true,
                    TrialEndsAt = DateTime.Now.AddDays(14),
                    CreatedAt = DateTime.Now
                };

                // 4. Crear el usuario Administrador del Taller
                User adminUser = new User
                {
                    Username = dto.AdminUsername,
                    Password = BCrypt.Net.BCrypt.HashPassword(dto.AdminPassword),
                    Email = dto.OwnerEmail,
                    FullName = "Administrador Principal",
                    FirstName = "Administrador",
                    FirstSurname = "Principal",
                    IdentificationNumber = "000000000",
                    IdentificationTypeId = 1,
                    UserRoleId = 2,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    MustChangePassword = false
                };

                workshop.Users.Add(adminUser);

                // 5. Crear settings por defecto
                workshop.Settings.Add(new ApiTaller.Domain.Models.WorkshopSettings { SettingKey = "currency", SettingValue = "COP", Description = "Moneda por defecto" });
                workshop.Settings.Add(new ApiTaller.Domain.Models.WorkshopSettings { SettingKey = "timezone", SettingValue = "America/Bogota", Description = "Zona horaria" });

                // 6. Guardar
                Domain.Models.Workshop savedWorkshop = await _workshopRepository.CreateAsync(workshop, ct);

                return new RegisterWorkshopResponseDto
                {
                    Success = true,
                    WorkshopId = savedWorkshop.Id,
                    Name = savedWorkshop.Name,
                    Slug = savedWorkshop.Slug,
                    WorkshopType = savedWorkshop.WorkshopType,
                    Plan = savedWorkshop.Plan,
                    Message = "Taller registrado con éxito."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registrando nuevo taller {WorkshopName}", dto.Name);
                return new RegisterWorkshopResponseDto { Success = false, Message = "Ocurrió un error interno al registrar el taller." };
            }
        }

        public async Task<WorkshopDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            Domain.Models.Workshop? workshop = await _workshopRepository.GetByIdAsync(id, ct);
            if (workshop == null) return null;

            return new WorkshopDto
            {
                Id = workshop.Id,
                Name = workshop.Name,
                Slug = workshop.Slug,
                OwnerEmail = workshop.OwnerEmail,
                Phone = workshop.Phone,
                Address = workshop.Address,
                City = workshop.City,
                WorkshopType = workshop.WorkshopType,
                Plan = workshop.Plan,
                IsActive = workshop.IsActive,
                TrialEndsAt = workshop.TrialEndsAt,
                CreatedAt = workshop.CreatedAt,
                TotalUsers = workshop.Users.Count
            };
        }

        public async Task<IEnumerable<WorkshopDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _workshopRepository.GetAllAsync(ct);
        }

        public async Task<bool> UpdateWorkshopAsync(int id, UpdateWorkshopDto dto, CancellationToken ct = default)
        {
            Domain.Models.Workshop? workshop = await _workshopRepository.GetByIdAsync(id, ct);
            if (workshop == null) return false;

            if (!string.IsNullOrEmpty(dto.Name)) workshop.Name = dto.Name;
            if (dto.Phone != null) workshop.Phone = dto.Phone;
            if (dto.Address != null) workshop.Address = dto.Address;
            if (dto.City != null) workshop.City = dto.City;

            if (!string.IsNullOrEmpty(dto.WorkshopType) && dto.WorkshopType != workshop.WorkshopType)
            {
                WorkshopTypeChangeValidationDto validation = await _workshopRepository.ValidateTypeChangeAsync(id, dto.WorkshopType, ct);
                if (!validation.CanChange)
                {
                    throw new InvalidOperationException(validation.Reason);
                }
                workshop.WorkshopType = dto.WorkshopType;
            }

            workshop.UpdatedAt = DateTime.Now;
            return await _workshopRepository.UpdateAsync(workshop, ct);
        }

        public async Task<bool> ToggleStatusAsync(int id, bool isActive, CancellationToken ct = default)
        {
            Domain.Models.Workshop? workshop = await _workshopRepository.GetByIdAsync(id, ct);
            if (workshop == null) return false;

            workshop.IsActive = isActive;
            workshop.UpdatedAt = DateTime.Now;
            return await _workshopRepository.UpdateAsync(workshop, ct);
        }

        public async Task<WorkshopTypeChangeValidationDto> ValidateTypeChangeAsync(int workshopId, string newType, CancellationToken ct = default)
        {
            return await _workshopRepository.ValidateTypeChangeAsync(workshopId, newType, ct);
        }

        public async Task<IEnumerable<WorkshopModuleDto>> GetAvailableModulesAsync(CancellationToken ct = default)
        {
            var modules = await _context.Module
                .IgnoreQueryFilters()
                .Where(m => m.IsActive && !ModuleConstants.SuperAdminReservedModules.Contains(m.Name))
                .OrderBy(m => m.Id)
                .ToListAsync(ct);

            return modules.Select(m => new WorkshopModuleDto
            {
                ModuleId = m.Id,
                ModuleName = m.Name,
                Category = GetCategoryForModule(m.Name),
                Description = GetDescriptionForModule(m.Name),
                IsEnabled = false,
                RequiredModuleNames = GetDependenciesForModule(m.Name)
            });
        }

        public async Task<IEnumerable<WorkshopModuleDto>> GetWorkshopModulesAsync(int workshopId, CancellationToken ct = default)
        {
            var allModules = await _context.Module
                .IgnoreQueryFilters()
                .Where(m => m.IsActive && !ModuleConstants.SuperAdminReservedModules.Contains(m.Name))
                .OrderBy(m => m.Id)
                .ToListAsync(ct);

            var activeWorkshopModuleIds = await _context.WorkshopModule
                .IgnoreQueryFilters()
                .Where(wm => wm.WorkshopId == workshopId && wm.IsActive)
                .Select(wm => wm.ModuleId)
                .ToListAsync(ct);

            var activeSet = new HashSet<int>(activeWorkshopModuleIds);

            return allModules.Select(m => new WorkshopModuleDto
            {
                ModuleId = m.Id,
                ModuleName = m.Name,
                Category = GetCategoryForModule(m.Name),
                Description = GetDescriptionForModule(m.Name),
                IsEnabled = activeSet.Contains(m.Id),
                RequiredModuleNames = GetDependenciesForModule(m.Name)
            });
        }

        public async Task<bool> UpdateWorkshopModulesAsync(int workshopId, UpdateWorkshopModulesDto dto, CancellationToken ct = default)
        {
            try
            {
                var workshop = await _context.Workshop
                    .IgnoreQueryFilters()
                    .FirstOrDefaultAsync(w => w.Id == workshopId, ct);

                if (workshop == null) return false;

                // 1. Resolver módulos permitidos y dependencias
                var allActiveModules = await _context.Module
                    .IgnoreQueryFilters()
                    .Where(m => m.IsActive && !ModuleConstants.SuperAdminReservedModules.Contains(m.Name))
                    .ToListAsync(ct);

                var requestedModules = allActiveModules.Where(m => dto.ModuleIds.Contains(m.Id)).ToList();
                var resolvedModuleIds = new HashSet<int>(requestedModules.Select(m => m.Id));
                var nameToModule = allActiveModules.ToDictionary(m => m.Name, m => m, StringComparer.OrdinalIgnoreCase);

                void EnsureModule(string moduleName)
                {
                    if (nameToModule.TryGetValue(moduleName, out var mod))
                        resolvedModuleIds.Add(mod.Id);
                }

                if (requestedModules.Any(m => m.Name.Equals(ModuleConstants.WorkOrders, StringComparison.OrdinalIgnoreCase)))
                {
                    EnsureModule(ModuleConstants.Customers);
                    EnsureModule(ModuleConstants.Vehicles);
                    EnsureModule(ModuleConstants.ServiceTypes);
                    EnsureModule(ModuleConstants.ServiceCatalogs);
                }

                if (requestedModules.Any(m => m.Name.Equals(ModuleConstants.Pos, StringComparison.OrdinalIgnoreCase)))
                {
                    EnsureModule(ModuleConstants.Products);
                    EnsureModule(ModuleConstants.Inventory);
                    EnsureModule(ModuleConstants.PaymentMethods);
                    EnsureModule(ModuleConstants.Customers);
                }

                if (requestedModules.Any(m => m.Name.Equals(ModuleConstants.Quotations, StringComparison.OrdinalIgnoreCase)))
                {
                    EnsureModule(ModuleConstants.Customers);
                    EnsureModule(ModuleConstants.Vehicles);
                    EnsureModule(ModuleConstants.Products);
                    EnsureModule(ModuleConstants.ServiceCatalogs);
                }

                if (requestedModules.Any(m => m.Name.Equals(ModuleConstants.Inventory, StringComparison.OrdinalIgnoreCase)))
                {
                    EnsureModule(ModuleConstants.Products);
                    EnsureModule(ModuleConstants.ProductTypes);
                }

                if (requestedModules.Any(m => m.Name.Equals(ModuleConstants.Accounting, StringComparison.OrdinalIgnoreCase)))
                {
                    EnsureModule(ModuleConstants.PaymentMethods);
                }

                if (requestedModules.Any(m => m.Name.Equals(ModuleConstants.Agenda, StringComparison.OrdinalIgnoreCase)))
                {
                    EnsureModule(ModuleConstants.Customers);
                    EnsureModule(ModuleConstants.Vehicles);
                }

                // 2. Sincronizar WorkshopModule
                var existingWorkshopModules = await _context.WorkshopModule
                    .IgnoreQueryFilters()
                    .Where(wm => wm.WorkshopId == workshopId)
                    .ToListAsync(ct);

                var existingActiveModuleIds = existingWorkshopModules.Where(wm => wm.IsActive).Select(wm => wm.ModuleId).ToHashSet();

                foreach (var mod in allActiveModules)
                {
                    bool shouldBeActive = resolvedModuleIds.Contains(mod.Id);
                    var existing = existingWorkshopModules.FirstOrDefault(wm => wm.ModuleId == mod.Id);

                    if (existing != null)
                    {
                        if (existing.IsActive != shouldBeActive)
                        {
                            existing.IsActive = shouldBeActive;
                            existing.UpdatedAt = DateTime.UtcNow;
                        }
                    }
                    else if (shouldBeActive)
                    {
                        _context.WorkshopModule.Add(new WorkshopModule
                        {
                            WorkshopId = workshopId,
                            ModuleId = mod.Id,
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }

                // 3. Cascada de revocación y activación en roles del taller
                var workshopRoles = await _context.UserRole
                    .IgnoreQueryFilters()
                    .Where(r => r.WorkshopId == workshopId)
                    .ToListAsync(ct);

                var roleIds = workshopRoles.Select(r => r.Id).ToList();

                var deactivatedModuleIds = existingActiveModuleIds.Except(resolvedModuleIds).ToList();
                var newlyActivatedModuleIds = resolvedModuleIds.Except(existingActiveModuleIds).ToList();

                // 3.1 Revocar módulos desactivados de TODOS los roles del taller
                if (deactivatedModuleIds.Any())
                {
                    var userRoleModulesToDeactivate = await _context.UserRoleModule
                        .IgnoreQueryFilters()
                        .Where(urm => roleIds.Contains(urm.UserRoleId) && deactivatedModuleIds.Contains(urm.ModulesRoleId))
                        .ToListAsync(ct);

                    foreach (var urm in userRoleModulesToDeactivate)
                    {
                        urm.IsActive = false;
                        urm.UpdatedAt = DateTime.UtcNow;
                    }

                    var actionsOfDeactivatedModules = await _context.Action
                        .IgnoreQueryFilters()
                        .Where(a => deactivatedModuleIds.Contains(a.ModuleId))
                        .Select(a => a.Id)
                        .ToListAsync(ct);

                    var roleActionsToDeactivate = await _context.RoleAction
                        .IgnoreQueryFilters()
                        .Where(ra => roleIds.Contains(ra.RoleId) && actionsOfDeactivatedModules.Contains(ra.ActionId))
                        .ToListAsync(ct);

                    foreach (var ra in roleActionsToDeactivate)
                    {
                        ra.IsActive = false;
                        ra.UpdatedAt = DateTime.UtcNow;
                    }
                }

                // 3.2 Habilitar módulos recién activados en el rol Administrador del taller
                var adminRole = workshopRoles.FirstOrDefault(r => r.Role.Equals("Administrador", StringComparison.OrdinalIgnoreCase));
                if (adminRole != null && newlyActivatedModuleIds.Any())
                {
                    var existingAdminRoleModules = await _context.UserRoleModule
                        .IgnoreQueryFilters()
                        .Where(urm => urm.UserRoleId == adminRole.Id)
                        .ToListAsync(ct);

                    foreach (var modId in newlyActivatedModuleIds)
                    {
                        var existingUrm = existingAdminRoleModules.FirstOrDefault(urm => urm.ModulesRoleId == modId);
                        if (existingUrm != null)
                        {
                            existingUrm.IsActive = true;
                            existingUrm.UpdatedAt = DateTime.UtcNow;
                        }
                        else
                        {
                            _context.UserRoleModule.Add(new UserRoleModule
                            {
                                UserRoleId = adminRole.Id,
                                ModulesRoleId = modId,
                                IsActive = true,
                                CreatedAt = DateTime.UtcNow
                            });
                        }
                    }

                    var actionsOfNewModules = await _context.Action
                        .IgnoreQueryFilters()
                        .Where(a => newlyActivatedModuleIds.Contains(a.ModuleId) && a.IsActive && a.Slug != "Guardar_Usuarios")
                        .ToListAsync(ct);

                    var existingAdminRoleActions = await _context.RoleAction
                        .IgnoreQueryFilters()
                        .Where(ra => ra.RoleId == adminRole.Id)
                        .ToListAsync(ct);

                    foreach (var act in actionsOfNewModules)
                    {
                        var existingRa = existingAdminRoleActions.FirstOrDefault(ra => ra.ActionId == act.Id);
                        if (existingRa != null)
                        {
                            existingRa.IsActive = true;
                            existingRa.UpdatedAt = DateTime.UtcNow;
                        }
                        else
                        {
                            _context.RoleAction.Add(new RoleAction
                            {
                                RoleId = adminRole.Id,
                                ActionId = act.Id,
                                IsActive = true,
                                CreatedAt = DateTime.UtcNow
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync(ct);

                // 4. Invalidar Caché en Memoria inmediatamente
                _memoryCache.Remove($"tenant_modules_{workshopId}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar módulos del taller {WorkshopId}", workshopId);
                throw;
            }
        }

        private static string GetCategoryForModule(string moduleName)
        {
            return moduleName switch
            {
                ModuleConstants.Customers or ModuleConstants.Vehicles or ModuleConstants.WorkOrders or ModuleConstants.Quotations or ModuleConstants.Pos or ModuleConstants.Agenda => "Operación Diaria",
                ModuleConstants.Inventory or ModuleConstants.Products or ModuleConstants.ProductTypes or ModuleConstants.Units or ModuleConstants.Suppliers => "Inventario y Repuestos",
                ModuleConstants.Accounting => "Contabilidad y Finanzas",
                ModuleConstants.Brands or ModuleConstants.Models or ModuleConstants.References or ModuleConstants.Cylinders or ModuleConstants.ServiceTypes or ModuleConstants.ServiceCatalogs or ModuleConstants.ServicePrices or ModuleConstants.PaymentMethods or ModuleConstants.WorkshopLogo or ModuleConstants.EmailSettings or ModuleConstants.CustomerPortal or ModuleConstants.ControlCenter => "Configuración y Catálogos",
                _ => "General"
            };
        }

        private static string GetDescriptionForModule(string moduleName)
        {
            return moduleName switch
            {
                ModuleConstants.Customers => "Gestión de clientes, historiales y datos de contacto.",
                ModuleConstants.Vehicles => "Registro de motos y vehículos, placas y propietarios.",
                ModuleConstants.WorkOrders => "Recepción de vehículos, evidencias fotográficas, servicios y repuestos.",
                ModuleConstants.Quotations => "Generación de presupuestos, envío por email y aprobación online.",
                ModuleConstants.Pos => "Punto de Venta directo de repuestos de mostrador.",
                ModuleConstants.Inventory => "Control de stock, alertas de mínimo y kardex de movimientos.",
                ModuleConstants.Agenda => "Citas y agendamiento de turnos para clientes.",
                ModuleConstants.Accounting => "Control de ventas, liquidación de mecánicos y flujo de caja.",
                _ => $"Gestión y parametrización de {moduleName}."
            };
        }

        private static List<string> GetDependenciesForModule(string moduleName)
        {
            return moduleName switch
            {
                ModuleConstants.WorkOrders => new List<string> { ModuleConstants.Customers, ModuleConstants.Vehicles, ModuleConstants.ServiceTypes, ModuleConstants.ServiceCatalogs },
                ModuleConstants.Pos => new List<string> { ModuleConstants.Products, ModuleConstants.Inventory, ModuleConstants.PaymentMethods, ModuleConstants.Customers },
                ModuleConstants.Quotations => new List<string> { ModuleConstants.Customers, ModuleConstants.Vehicles, ModuleConstants.Products, ModuleConstants.ServiceCatalogs },
                ModuleConstants.Inventory => new List<string> { ModuleConstants.Products, ModuleConstants.ProductTypes },
                ModuleConstants.Agenda => new List<string> { ModuleConstants.Customers, ModuleConstants.Vehicles },
                ModuleConstants.Accounting => new List<string> { ModuleConstants.PaymentMethods },
                _ => new List<string>()
            };
        }

        private string GenerateSlug(string name)
        {
            string slug = name.ToLowerInvariant();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');
            return slug;
        }
    }
}
