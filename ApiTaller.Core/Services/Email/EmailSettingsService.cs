using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using ApiTaller.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Email
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailSender;

        public EmailSettingsService(DataContext context, IEmailService emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<EmailSettings> GetSettingsAsync(CancellationToken ct)
        {
            var settings = await _context.EmailSettings.FirstOrDefaultAsync(ct);
            if (settings != null)
            {
                settings.Password = "********";
            }
            return settings;
        }

        public async Task<bool> SaveSettingsAsync(EmailSettings settings, CancellationToken ct)
        {
            var existing = await _context.EmailSettings.FirstOrDefaultAsync(ct);

            if (settings.Password == "********" && existing != null)
            {
                settings.Password = existing.Password;
            }
            else
            {
                settings.Password = SecurityHelper.Encrypt(settings.Password);
            }

            if (existing == null)
            {
                settings.CreatedAt = DateTime.Now;
                settings.IsActive = true;
                await _context.EmailSettings.AddAsync(settings, ct);
            }
            else
            {
                existing.Host = settings.Host;
                existing.Port = settings.Port;
                existing.UserName = settings.UserName;
                existing.Password = settings.Password;
                existing.EnableSsl = settings.EnableSsl;
                existing.SenderName = settings.SenderName;
                existing.SenderEmail = settings.SenderEmail;
                existing.UpdatedAt = DateTime.Now;
            }

            return await _context.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> TestConnectionAsync(EmailSettings settings, CancellationToken ct)
        {
            var existing = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync(ct);
            
            if (settings.Password == "********" && existing != null)
            {
                settings.Password = existing.Password;
            }

            return await _emailSender.TestConnectionAsync(settings, ct);
        }
    }
}
