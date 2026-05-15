using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using ApiTaller.Infrastructure.Helpers;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly DataContext _context;

        public EmailService(DataContext context)
        {
            _context = context;
        }

        public async Task SendEmailAsync(EmailRequest request, CancellationToken ct = default)
        {
            var config = await _context.EmailSettings.FirstOrDefaultAsync(x => x.IsActive, ct);
            if (config == null)
            {
                throw new Exception("No hay una configuración de correo activa.");
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(config.SenderName, config.SenderEmail));
            message.To.Add(new MailboxAddress("", request.To));
            message.Subject = request.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = request.Body };

            if (request.Attachments != null)
            {
                foreach (var attachment in request.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.ContentType));
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(config.Host, config.Port, config.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto, ct);
                
                string decryptedPassword = SecurityHelper.Decrypt(config.Password);
                await client.AuthenticateAsync(config.UserName, decryptedPassword, ct);
                
                await client.SendAsync(message, ct);
                await client.DisconnectAsync(true, ct);
            }
        }

        public async Task<bool> TestConnectionAsync(EmailSettings settings, CancellationToken ct = default)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(settings.Host, settings.Port, settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto, ct);
                    
                    string passwordToUse = settings.Password;
                    try {
                        passwordToUse = SecurityHelper.Decrypt(settings.Password);
                    } catch { }

                    await client.AuthenticateAsync(settings.UserName, passwordToUse, ct);
                    await client.DisconnectAsync(true, ct);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error testing SMTP: {ex.Message}");
                return false;
            }
        }
    }
}
