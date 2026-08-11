using ApiTaller.Domain.Dtos.WorkshopConfig;
using ApiTaller.Domain.Interfaces.Repositories.EmailSettings;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Email
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private readonly IEmailSettingsRepository _emailSettingsRepository;
        private readonly IEmailService _emailSender;
        private readonly ICurrentUserService _currentUserService;

        public EmailSettingsService(
            IEmailSettingsRepository emailSettingsRepository, 
            IEmailService emailSender, 
            ICurrentUserService currentUserService)
        {
            _emailSettingsRepository = emailSettingsRepository;
            _emailSender = emailSender;
            _currentUserService = currentUserService;
        }

        public async Task<EmailSettingsDto?> GetSettingsAsync(CancellationToken ct)
        {
            Domain.Models.EmailSettings? settings = await _emailSettingsRepository.GetSettingsAsync(ct);
            if (settings == null) return null;

            return new EmailSettingsDto
            {
                Id = settings.Id,
                Host = settings.Host,
                Port = settings.Port,
                UserName = settings.UserName,
                Password = "********", // Enmascarado en capa de presentación/DTO, sin corromper el modelo de base de datos
                EnableSsl = settings.EnableSsl,
                SenderName = settings.SenderName,
                SenderEmail = settings.SenderEmail,
                IsActive = settings.IsActive,
                ResponsibleUserId = settings.ResponsibleUserId
            };
        }

        public async Task<bool> SaveSettingsAsync(EmailSettingsDto dto, CancellationToken ct)
        {
            Domain.Models.EmailSettings? existing = await _emailSettingsRepository.GetSettingsAsync(ct);

            string finalPassword;
            if (dto.Password == "********" && existing != null)
            {
                finalPassword = existing.Password;
            }
            else
            {
                finalPassword = SecurityHelper.Encrypt(dto.Password);
            }

            int? userId = null;
            if (int.TryParse(_currentUserService.UserId, out int parsedId))
                userId = parsedId;

            Domain.Models.EmailSettings settings = new Domain.Models.EmailSettings
            {
                Id = dto.Id,
                Host = dto.Host,
                Port = dto.Port,
                UserName = dto.UserName,
                Password = finalPassword,
                EnableSsl = dto.EnableSsl,
                SenderName = dto.SenderName,
                SenderEmail = dto.SenderEmail,
                IsActive = true,
                ResponsibleUserId = userId
            };

            return await _emailSettingsRepository.SaveSettingsAsync(settings, ct);
        }

        public async Task<bool> TestConnectionAsync(EmailSettingsDto dto, CancellationToken ct)
        {
            Domain.Models.EmailSettings? existing = await _emailSettingsRepository.GetSettingsAsync(ct);

            string finalPassword;
            if (dto.Password == "********" && existing != null)
            {
                finalPassword = existing.Password;
            }
            else
            {
                finalPassword = SecurityHelper.Encrypt(dto.Password);
            }

            Domain.Models.EmailSettings settings = new Domain.Models.EmailSettings
            {
                Host = dto.Host,
                Port = dto.Port,
                UserName = dto.UserName,
                Password = finalPassword,
                EnableSsl = dto.EnableSsl,
                SenderName = dto.SenderName,
                SenderEmail = dto.SenderEmail
            };

            return await _emailSender.TestConnectionAsync(settings, ct);
        }
    }
}
