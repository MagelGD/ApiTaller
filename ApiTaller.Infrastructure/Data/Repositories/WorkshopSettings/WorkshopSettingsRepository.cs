using ApiTaller.Domain.Dtos.WorkshopConfig;
using ApiTaller.Domain.Interfaces.Repositories.WorkshopSettings;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Data.Repositories.WorkshopSettings
{
    public class WorkshopSettingsRepository : IWorkshopSettingsRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<WorkshopSettingsRepository> _logger;
        private readonly ICurrentUserService _currentUserService;

        public WorkshopSettingsRepository(DataContext context, ILogger<WorkshopSettingsRepository> logger, ICurrentUserService currentUserService)
        {
            _context = context;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<WorkshopSettingsDto>> GetAllAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.WorkshopSettings
                    .Where(s => s.IsActive)
                    .Select(setting => new WorkshopSettingsDto
                    {
                        Id = setting.Id,
                        SettingKey = setting.SettingKey,
                        SettingValue = setting.SettingValue,
                        Description = setting.Description,
                        IsActive = setting.IsActive,
                        CreatedAt = setting.CreatedAt,
                        UpdatedAt = setting.UpdatedAt,
                        ResponsibleUserId = setting.ResponsibleUserId
                    })
                    .ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all workshop settings");
                return Enumerable.Empty<WorkshopSettingsDto>();
            }
        }

        public async Task<WorkshopSettingsDto?> GetByKeyAsync(string key, CancellationToken cancellation)
        {
            try
            {
                Domain.Models.WorkshopSettings? setting = await _context.WorkshopSettings
                    .FirstOrDefaultAsync(s => s.SettingKey == key && s.IsActive, cancellation);

                if (setting == null) return null;

                return new WorkshopSettingsDto
                {
                    Id = setting.Id,
                    SettingKey = setting.SettingKey,
                    SettingValue = setting.SettingValue,
                    Description = setting.Description,
                    IsActive = setting.IsActive,
                    CreatedAt = setting.CreatedAt,
                    UpdatedAt = setting.UpdatedAt,
                    ResponsibleUserId = setting.ResponsibleUserId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting workshop setting by key '{key}'");
                return null;
            }
        }

        public async Task<bool> UpsertAsync(WorkshopSettingsDto dto, CancellationToken cancellation)
        {
            int? userId = null;
            if (int.TryParse(_currentUserService.UserId, out int parsedId))
                userId = parsedId;

            Domain.Models.WorkshopSettings? existing = await _context.WorkshopSettings
                .FirstOrDefaultAsync(s => s.SettingKey == dto.SettingKey, cancellation);

            if (existing != null)
            {
                // UPDATE
                existing.SettingValue = dto.SettingValue;
                existing.Description = dto.Description;
                existing.UpdatedAt = DateTime.Now;
                existing.IsActive = true;
                if (userId.HasValue) existing.ResponsibleUserId = userId;
            }
            else
            {
                // INSERT
                Domain.Models.WorkshopSettings newSetting = new Domain.Models.WorkshopSettings
                {
                    SettingKey = dto.SettingKey,
                    SettingValue = dto.SettingValue,
                    Description = dto.Description,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    ResponsibleUserId = userId
                };
                await _context.WorkshopSettings.AddAsync(newSetting, cancellation);
            }

            return await _context.SaveChangesAsync(cancellation) > 0;
        }
    }
}
