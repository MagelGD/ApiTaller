using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
using ApiTaller.Infrastructure.Data;
using ApiTaller.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSettingsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;

        public EmailSettingsController(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var settings = await _context.EmailSettings.FirstOrDefaultAsync();
            if (settings == null) return NotFound(new { message = "No hay configuración cargada" });
            
            // Enmascaramos la contraseña por seguridad
            settings.Password = "********"; 
            return Ok(settings);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmailSettings settings)
        {
            var existing = await _context.EmailSettings.FirstOrDefaultAsync();
            
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
                _context.EmailSettings.Add(settings);
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

            await _context.SaveChangesAsync();
            return Ok(new { message = "Configuración guardada correctamente" });
        }

        [HttpPost("test")]
        public async Task<IActionResult> Test([FromBody] EmailSettings settings)
        {
            var existing = await _context.EmailSettings.AsNoTracking().FirstOrDefaultAsync();
            
            // Si es una prueba de configuración ya guardada
            if (settings.Password == "********" && existing != null)
            {
                settings.Password = existing.Password;
            }

            bool success = await _emailService.TestConnectionAsync(settings);
            if (success) return Ok(new { message = "Conexión exitosa" });
            return BadRequest(new { message = "Error al conectar con el servidor SMTP. Verifique sus credenciales." });
        }
    }
}
