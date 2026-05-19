using ApiTaller.Domain.Dtos.Customer;
using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Customers;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services.Customers;
using ApiTaller.Domain.Interfaces.Services.Email;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.Core.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IEmailService _emailService;

        public CustomerService(
            ICustomerRepository customerRepository, 
            ILogger<CustomerService> logger, 
            IUserRepository userRepository, 
            IUserRoleRepository userRoleRepository,
            IEmailService emailService)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _emailService = emailService;
        }

        public async Task<GetCustomerDto> CreateOrEditCustomer(GetCustomerDto customer, CancellationToken cancellationToken)
        {
            GetCustomerDto result = new();
            try
            {
                Customer saveData = new()
                {
                    Id = customer.Id,
                    UserId = customer.UserId,
                    IdentificationTypeId = customer.IdentificationTypeId,
                    IdentificationNumber = customer.IdentificationNumber,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber,
                    Email = customer.Email,
                    Address = customer.Address,
                    IsActive = customer.IsActive,
                    CreatedAt = customer.CreatedAt ?? DateTime.Now
                };

                bool isExist = await ValidateExist(customer, cancellationToken);
                bool wasUserCreated = false;
                User? savedUser = null;

                if (saveData.Id == 0 && !isExist)
                {
                    if (!await ValidateUserExist(saveData.UserId, cancellationToken) && !await ValidateDocumentUserExist(saveData.IdentificationNumber, saveData.IdentificationNumber, cancellationToken))
                    {
                        savedUser = new User()
                        {
                            Id = 0,
                            UserRoleId = await GetCustomerUserRoleId(cancellationToken),
                            IdentificationTypeId = customer.IdentificationTypeId,
                            IdentificationNumber = customer.IdentificationNumber,
                            FirstName = customer.FirstName,
                            MiddleName = string.Empty,
                            FirstSurname = customer.LastName,
                            SecondLastName = string.Empty,
                            FullName = $"{customer.FirstName} {customer.LastName}",
                            Username = customer.IdentificationNumber,
                            Password = BCrypt.Net.BCrypt.HashPassword(customer.IdentificationNumber),
                            Email = customer.Email,
                            IsActive = true,
                            MustChangePassword = true,
                            CreatedAt = DateTime.Now
                        };
                        await _userRepository.CreateUser(savedUser, cancellationToken);
                        GetUsersDto? userCreated = await _userRepository.ValidateExist(savedUser.Username, savedUser.IdentificationNumber, cancellationToken);
                        saveData.UserId = userCreated?.Id ?? 0;

                        if (userCreated != null)
                        {
                            savedUser.Id = userCreated.Id;
                            wasUserCreated = true;
                        }
                    }
                    await _customerRepository.CreateAsync(saveData, cancellationToken);

                    // Send welcome email if the user was successfully created
                    if (wasUserCreated && savedUser != null && !string.IsNullOrEmpty(savedUser.Email))
                    {
                        await SendWelcomeEmailAsync(savedUser, customer.IdentificationNumber, cancellationToken);
                    }
                }
                else if (saveData.Id != 0)
                {
                    await _customerRepository.UpdateAsync(saveData, cancellationToken);
                }

                result = await _customerRepository.ValidateExist(customer, cancellationToken) ?? new GetCustomerDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear o editar el cliente con identificación {customer.IdentificationNumber}");
            }
            return result;
        }

        public async Task<bool> ResendWelcomeEmailAsync(int customerId, CancellationToken cancellation)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(customerId, cancellation);
                if (customer == null)
                {
                    _logger.LogWarning("ResendWelcomeEmail: No se encontró el cliente con ID {CustomerId}", customerId);
                    return false;
                }

                // Obtener el usuario asociado por documento o username
                var userDto = await _userRepository.ValidateExist(customer.IdentificationNumber, customer.IdentificationNumber, cancellation);
                if (userDto == null)
                {
                    _logger.LogWarning("ResendWelcomeEmail: No se encontró el usuario asociado a la identificación {Doc}", customer.IdentificationNumber);
                    return false;
                }

                // Reconstruir un objeto User para reutilizar SendWelcomeEmailAsync
                User user = new User
                {
                    Id = userDto.Id,
                    Username = userDto.Username,
                    FullName = userDto.FullName,
                    Email = userDto.Email ?? customer.Email
                };

                // Enviar el correo de bienvenida de forma síncrona para retroalimentación instantánea
                await SendWelcomeEmailAsync(user, customer.IdentificationNumber, cancellation);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reenviar correo de bienvenida para el cliente {CustomerId}", customerId);
                return false;
            }
        }

        private async Task SendWelcomeEmailAsync(User user, string temporaryPassword, CancellationToken cancellationToken)
        {
            try
            {
                var loginUrl = "http://localhost:4200/portal/login";
                var emailRequest = new EmailRequest
                {
                    To = user.Email,
                    Subject = "¡Bienvenido a Deivid Motos! — Tus credenciales de acceso",
                    Body = $@"
                    <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #e0e0e0; border-radius: 8px; background-color: #ffffff;'>
                        <div style='text-align: center; margin-bottom: 20px;'>
                            <h2 style='color: #0ea5e9; margin: 0;'>Deivid Motos</h2>
                            <p style='color: #6b7280; font-size: 14px; margin: 5px 0 0 0;'>Portal Cliente</p>
                        </div>
                        <hr style='border: 0; border-top: 1px solid #e0e0e0; margin-bottom: 20px;' />
                        <p style='color: #374151; font-size: 16px; line-height: 1.5;'>Hola <strong>{user.FullName}</strong>,</p>
                        <p style='color: #374151; font-size: 16px; line-height: 1.5;'>¡Te damos una cálida bienvenida a Deivid Motos! Hemos creado tu cuenta en nuestro Portal de Clientes para que puedas gestionar tus vehículos, citas e historial de taller en tiempo real.</p>
                        <p style='color: #374151; font-size: 16px; line-height: 1.5;'>A continuación, encontrarás tus credenciales temporales de acceso:</p>
                        
                        <div style='background-color: #f3f4f6; border-radius: 6px; padding: 15px; margin: 20px 0;'>
                            <p style='margin: 0 0 8px 0; font-size: 15px; color: #374151;'><strong>Usuario:</strong> {user.Username}</p>
                            <p style='margin: 0; font-size: 15px; color: #374151;'><strong>Contraseña temporal:</strong> {temporaryPassword}</p>
                        </div>

                        <div style='text-align: center; margin: 30px 0;'>
                            <a href='{loginUrl}' style='background-color: #0ea5e9; color: #ffffff; text-decoration: none; padding: 12px 24px; border-radius: 6px; font-weight: bold; font-size: 16px; display: inline-block;'>Ingresar al Portal</a>
                        </div>
                        
                        <p style='color: #ef4444; font-size: 14px; line-height: 1.5; font-weight: bold;'>⚠️ Por motivos de seguridad, el sistema te solicitará cambiar tu contraseña de forma obligatoria al ingresar por primera vez.</p>
                        <p style='color: #6b7280; font-size: 14px; line-height: 1.5; margin-top: 20px;'>Si tienes dudas o inconvenientes para ingresar, por favor comunícate con nosotros respondiendo a este correo.</p>
                        <hr style='border: 0; border-top: 1px solid #e0e0e0; margin: 25px 0 15px 0;' />
                        <p style='text-align: center; color: #9ca3af; font-size: 12px; margin: 0;'>Deivid Motos PWA — Todos los derechos reservados.</p>
                    </div>"
                };

                await _emailService.SendEmailAsync(emailRequest, cancellationToken);
                _logger.LogInformation("Correo electrónico de bienvenida enviado con éxito a {Email}", user.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar el correo electrónico de bienvenida para {Email}", user.Email);
            }
        }

        private async Task<int> GetCustomerUserRoleId(CancellationToken cancellation)
        {
            int result = 0;
            try
            {
                GetUserRoleDto? userRole = await _userRoleRepository.GetUserRoleName("Cliente", cancellation);
                if (userRole != null)
                {
                    result = userRole.IdUserRol;
                }
                else
                {
                    _logger.LogWarning("No se encontró el rol 'Cliente' en la base de datos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el ID del rol 'Cliente'");
            }
            return result;
        }

        private async Task<bool> ValidateUserExist(int userId, CancellationToken cancellation)
        {
            bool result = false;
            try
            {
                GetUsersDto? data = await _userRepository.GetUserById(userId, cancellation);
                result = data != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar la existencia del usuario con ID {userId}");
            }
            return result;
        }

        private async Task<bool> ValidateDocumentUserExist(string document, string username, CancellationToken cancellation)
        {
            bool result = false;
            try
            {
                GetUsersDto? data = await _userRepository.ValidateExist(username, document, cancellation);
                result = data != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar la existencia");
            }
            return result;
        }

        public async Task<IEnumerable<GetCustomerDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetCustomerDto> result = [];
            try
            {
                result = await _customerRepository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los clientes activos");
            }
            return result;
        }

        public async Task<IEnumerable<GetCustomerDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetCustomerDto> result = [];
            try
            {
                result = await _customerRepository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los clientes");
            }
            return result;
        }

        public async Task<GetCustomerDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetCustomerDto? result = null;
            try
            {
                result = await _customerRepository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el cliente con ID {id}");
            }
            return result;
        }

        private async Task<bool> ValidateExist(GetCustomerDto data, CancellationToken cancellation)
        {
            bool result = false;
            try
            {
                var existingCustomer = await _customerRepository.ValidateExist(data, cancellation);
                result = existingCustomer != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al validar la existencia del cliente con identificación {data.IdentificationNumber}");
            }
            return result;
        }
    }
}
