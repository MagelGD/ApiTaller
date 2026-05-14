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

        public async Task SendEmailAsync(EmailRequest request)
        {
            var config = await _context.EmailSettings.FirstOrDefaultAsync(x => x.IsActive);
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

                await client.ConnectAsync(config.Host, config.Port, config.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
                
                string decryptedPassword = SecurityHelper.Decrypt(config.Password);
                await client.AuthenticateAsync(config.UserName, decryptedPassword);
                
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task<bool> TestConnectionAsync(EmailSettings settings)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(settings.Host, settings.Port, settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
                    
                    // Si la contraseña ya viene encriptada (desde el form si es una edición), la desencriptamos. 
                    // Si viene plana (nueva configuración), la usamos plana para el test.
                    string passwordToUse = settings.Password;
                    try {
                        passwordToUse = SecurityHelper.Decrypt(settings.Password);
                    } catch {
                        // Si falla es que estaba plana
                    }

                    await client.AuthenticateAsync(settings.UserName, passwordToUse);
                    await client.DisconnectAsync(true);
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
