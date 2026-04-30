using ApiTaller.Domain.Dtos.Customer;
using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Customers;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services.Customers;
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

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
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

                if (saveData.Id == 0 && !isExist)
                {
                    if (!await ValidateUserExist(saveData.UserId, cancellationToken) && !await ValidateDocumentUserExist(saveData.IdentificationNumber, saveData.IdentificationNumber, cancellationToken))
                    {
                        User saveDataUser = new()
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
                            CreatedAt = DateTime.Now
                        };
                        await _userRepository.CreateUser(saveDataUser, cancellationToken);
                        GetUsersDto? userCreated = await _userRepository.ValidateExist(saveDataUser.Username, saveDataUser.IdentificationNumber, cancellationToken);
                        saveData.UserId = userCreated?.Id ?? 0;
                    }
                    await _customerRepository.CreateAsync(saveData, cancellationToken);
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
                    _logger.LogWarning("No se encontró el rol 'Customer' en la base de datos");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el ID del rol 'Customer'");
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
