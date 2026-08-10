using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Repositories.Workshop;
using ApiTaller.Domain.Interfaces.Services.Workshop;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace ApiTaller.Core.Services.Workshop
{
    public class WorkshopService : IWorkshopService
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly ILogger<WorkshopService> _logger;

        public WorkshopService(IWorkshopRepository workshopRepository, ILogger<WorkshopService> logger)
        {
            _workshopRepository = workshopRepository;
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
                    slug = $"{slug}-{DateTime.Now.Ticks.ToString().Substring(10)}"; // Agregar un sufijo único si existe
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
                    TrialEndsAt = DateTime.Now.AddDays(14), // 14 días de prueba
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
                    IdentificationTypeId = 1, // CC (Ajustar según catálogo)
                    UserRoleId = 2, // Role 'Administrador' (Ajustar según catálogo, asumiendo 1 = SuperAdmin plataforma, 2 = Admin Tenant)
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    MustChangePassword = false
                };

                workshop.Users.Add(adminUser);

                // 5. Crear settings por defecto (ejemplo)
                workshop.Settings.Add(new ApiTaller.Domain.Models.WorkshopSettings { SettingKey = "currency", SettingValue = "COP", Description = "Moneda por defecto" });
                workshop.Settings.Add(new ApiTaller.Domain.Models.WorkshopSettings { SettingKey = "timezone", SettingValue = "America/Bogota", Description = "Zona horaria" });

                // 6. Guardar todo en la base de datos (se guarda el workshop y en cascada el usuario admin y los settings)
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
            WorkshopDto? workshop = await _workshopRepository.GetByIdAsync(id, ct);
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
            WorkshopDto? workshop = await _workshopRepository.GetByIdAsync(id, ct);
            if (workshop == null) return false;

            if (!string.IsNullOrEmpty(dto.Name)) workshop.Name = dto.Name;
            if (dto.Phone != null) workshop.Phone = dto.Phone;
            if (dto.Address != null) workshop.Address = dto.Address;
            if (dto.City != null) workshop.City = dto.City;

            // Lógica especial para cambio de tipo
            if (!string.IsNullOrEmpty(dto.WorkshopType) && dto.WorkshopType != workshop.WorkshopType)
            {
                TypeChangeValidationDto validation = await _workshopRepository.ValidateTypeChangeAsync(id, dto.WorkshopType, ct);
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
            WorkshopDto? workshop = await _workshopRepository.GetByIdAsync(id, ct);
            if (workshop == null) return false;

            workshop.IsActive = isActive;
            workshop.UpdatedAt = DateTime.Now;
            return await _workshopRepository.UpdateAsync(workshop, ct);
        }

        public async Task<WorkshopTypeChangeValidationDto> ValidateTypeChangeAsync(int workshopId, string newType, CancellationToken ct = default)
        {
            return await _workshopRepository.ValidateTypeChangeAsync(workshopId, newType, ct);
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
