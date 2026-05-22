using ApiTaller.Domain.Interfaces.Repositories.EmailSettings;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.EmailSettings
{
    public class EmailSettingsRepository : IEmailSettingsRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<EmailSettingsRepository> _logger;

        public EmailSettingsRepository(DataContext context, ILogger<EmailSettingsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Domain.Models.EmailSettings?> GetSettingsAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.EmailSettings.FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la configuración de correo desde la base de datos");
                return null;
            }
        }

        public async Task<bool> SaveSettingsAsync(Domain.Models.EmailSettings settings, CancellationToken cancellation)
        {
            try
            {
                // Cargar el usuario responsable desde el DbContext para que esté trackeado
                // y EF Core configure correctamente la propiedad de sombra no nula 'ResponsibleUserIdNavigationId'
                if (settings.ResponsibleUserId.HasValue)
                {
                    var user = await _context.User.FindAsync(new object[] { settings.ResponsibleUserId.Value }, cancellation);
                    if (user != null)
                    {
                        settings.ResponsibleUserIdNavigation = user;
                    }
                }

                var existing = await _context.EmailSettings.FirstOrDefaultAsync(cancellation);

                if (existing == null)
                {
                    settings.CreatedAt = DateTime.Now;
                    settings.IsActive = true;
                    await _context.EmailSettings.AddAsync(settings, cancellation);
                }
                else
                {
                    _context.Entry(existing).State = EntityState.Detached; // Evita conflictos de tracking
                    settings.Id = existing.Id;
                    settings.CreatedAt = existing.CreatedAt;
                    settings.UpdatedAt = DateTime.Now;
                    settings.IsActive = true;
                    _context.EmailSettings.Update(settings);
                }

                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar la configuración de correo en la base de datos");
                return false;
            }
        }
    }
}
