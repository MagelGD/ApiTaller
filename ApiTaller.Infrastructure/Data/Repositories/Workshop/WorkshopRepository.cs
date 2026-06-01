using ApiTaller.Domain.Dtos.Workshop;
using ApiTaller.Domain.Interfaces.Repositories.Workshop;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ApiTaller.Infrastructure.Data.Repositories.Workshop
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly DataContext _context;

        public WorkshopRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Domain.Models.Workshop?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Workshop
                .Include(w => w.Users) // Opcional, para contar usuarios
                .FirstOrDefaultAsync(w => w.Id == id, ct);
        }

        public async Task<Domain.Models.Workshop?> GetBySlugAsync(string slug, CancellationToken ct = default)
        {
            return await _context.Workshop
                .FirstOrDefaultAsync(w => w.Slug == slug && w.IsActive, ct);
        }

        public async Task<IEnumerable<WorkshopDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Workshop
                .Select(w => new WorkshopDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Slug = w.Slug,
                    OwnerEmail = w.OwnerEmail,
                    Phone = w.Phone,
                    Address = w.Address,
                    City = w.City,
                    WorkshopType = w.WorkshopType,
                    Plan = w.Plan,
                    IsActive = w.IsActive,
                    TrialEndsAt = w.TrialEndsAt,
                    CreatedAt = w.CreatedAt,
                    TotalUsers = w.Users.Count
                })
                .ToListAsync(ct);
        }

        public async Task<Domain.Models.Workshop> CreateAsync(Domain.Models.Workshop workshop, CancellationToken ct = default)
        {
            await _context.Workshop.AddAsync(workshop, ct);
            await _context.SaveChangesAsync(ct);
            return workshop;
        }

        public async Task<bool> UpdateAsync(Domain.Models.Workshop workshop, CancellationToken ct = default)
        {
            _context.Workshop.Update(workshop);
            return await _context.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> SlugExistsAsync(string slug, CancellationToken ct = default)
        {
            return await _context.Workshop.AnyAsync(w => w.Slug == slug, ct);
        }

        public async Task<WorkshopTypeChangeValidationDto> ValidateTypeChangeAsync(int workshopId, string newType, CancellationToken ct = default)
        {
            var result = new WorkshopTypeChangeValidationDto
            {
                RequestedType = newType,
                CanChange = true
            };

            var workshop = await _context.Workshop.FirstOrDefaultAsync(w => w.Id == workshopId, ct);
            if (workshop == null)
            {
                result.CanChange = false;
                result.Reason = "Taller no encontrado.";
                return result;
            }

            result.CurrentType = workshop.WorkshopType;

            // Si es el mismo tipo, no hay problema
            if (workshop.WorkshopType == newType)
            {
                result.Reason = "El tipo de taller ya es el solicitado.";
                return result;
            }

            // 'multi' puede hacer de todo, no se puede degradar a 'moto' o 'car' si ya hay datos cruzados
            // Para simplificar: Si eres 'moto' puedes pasar a 'multi'. Si eres 'car' puedes pasar a 'multi'.
            // Bajar de 'multi' a 'moto' o 'car' REQUIERE validar si hay datos incompatibles.

            // Vamos a contar cuántas motos y carros hay registrados (ignorando el _tenantContext porque ya tenemos el ID exacto)
            result.MotorcycleCount = await _context.Vehicle.IgnoreQueryFilters().CountAsync(v => v.WorkshopId == workshopId && v.VehicleType == "moto", ct);
            result.CarCount = await _context.Vehicle.IgnoreQueryFilters().CountAsync(v => v.WorkshopId == workshopId && v.VehicleType == "car", ct);

            if (newType == "moto" && result.CarCount > 0)
            {
                result.CanChange = false;
                result.Reason = $"No se puede cambiar a tipo 'moto' porque el taller ya tiene {result.CarCount} vehículo(s) tipo 'car' registrados.";
            }
            else if (newType == "car" && result.MotorcycleCount > 0)
            {
                result.CanChange = false;
                result.Reason = $"No se puede cambiar a tipo 'car' porque el taller ya tiene {result.MotorcycleCount} motocicleta(s) registradas.";
            }

            return result;
        }
    }
}
