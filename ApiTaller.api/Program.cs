using ApiTaller.Core.Services.Auth;
using ApiTaller.Core.Services.ServiceConfigurations;
using ApiTaller.Core.Services.Users;
using ApiTaller.Domain.Dtos.Options;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services.Auth;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Infrastructure.Data;
using ApiTaller.Infrastructure.Data.Repositories.RepositoryConfigurations;
using ApiTaller.Infrastructure.Data.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Infrastructure.Security;
using ApiTaller.api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;
using ApiTaller.api.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Rate Limiting por NOMBRE DE USUARIO — protege el login contra fuerza bruta
// sin afectar a otros usuarios. Cada username tiene su propio contador de 5 intentos.
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("LoginPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            // Clave de partición: el username en minúsculas.
            // Si no viene en el header (primera solicitud), usa la IP como fallback.
            partitionKey: httpContext.Request.Headers["X-Username"].FirstOrDefault()?.ToLowerInvariant()
                          ?? httpContext.Connection.RemoteIpAddress?.ToString()
                          ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,                           // Máximo 5 intentos por usuario
                Window = TimeSpan.FromMinutes(5),          // Ventana de 5 minutos
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0                             // Rechazar de inmediato, sin cola
            }));

    // Respuesta cuando el cliente supera el límite
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.Headers["Retry-After"] = "300";
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsync(
            "{\"message\": \"Demasiados intentos. Por favor, espera 5 minutos.\"}",
            cancellationToken: token);
    };
});

// Filtro global de permisos dinámicos — solo actúa si el endpoint tiene [RequirePermission("slug")]
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionFilter>();
});
builder.Services.AddSignalR();
builder.Services.AddOpenApi();
#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
               .SetIsOriginAllowed(_ => true) // Allow any origin (development only)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });

});
#endregion
#region Inject Services
builder.Services.AddServices();
builder.Services.AddScoped<ApiTaller.Domain.Interfaces.Services.WorkOrders.IWorkOrderNotificationService, ApiTaller.api.Services.WorkOrderNotificationService>();
#endregion
#region Inject Repositories
builder.Services.AddRespositories();
#endregion
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Auth"));

builder.Services.AddDbContext<DataContext>(options =>
        options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            new MariaDbServerVersion(new Version(10, 4, 32))
        )
);
// HttpContext accessor and current user service for per-request user info (claims)
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ITenantContext, TenantContext>(); // SAAS-1: TenantContext para Global Query Filters
#region Inject JwtOptions
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Auth"));
var jwtOptions = builder.Configuration
    .GetSection("Auth")
    .Get<JwtOptions>() ?? throw new InvalidOperationException("No se pudieron cargar las opciones de JWT");
#endregion
#region Validación de token 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.JwtSigningKey)),
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Rate limiting nativo aplicado antes de autenticación
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PermissionsHub>("/hubs/permissions");
app.MapHub<WorkOrderHub>("/hubs/work-orders");

app.Run();
