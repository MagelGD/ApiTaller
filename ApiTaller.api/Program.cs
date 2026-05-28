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
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiTaller.api.Hubs;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Rate Limiting — protege el endpoint de login contra fuerza bruta
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.RealIpHeader = "X-Forwarded-For"; // Soporte para proxies / balanceadores
    options.ClientIdHeader = "X-ClientId";
    options.HttpStatusCode = 429;
    
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*:/api/auth/login", // Siempre minúsculas para AspNetCoreRateLimit
            Limit    = 5,    // 5 intentos
            Period   = "5m"  // por IP cada 5 minutos
        }
    };
});
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

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

// Rate limiting aplicado antes de autenticación
app.UseIpRateLimiting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PermissionsHub>("/hubs/permissions");
app.MapHub<WorkOrderHub>("/hubs/work-orders");
app.Run();
