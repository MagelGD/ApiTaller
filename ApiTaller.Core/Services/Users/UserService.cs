using ApiTaller.Domain.Common.Constants;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services.Users;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<GetUsersDto?> CreateOrEditUser(GetUsersDto userDto, CancellationToken cancellation = default)
        {
            GetUsersDto? result = null;
            try
            {
                User saveData = new()
                {
                    Id = userDto.Id,
                    UserRoleId = userDto.UserRoleId,
                    IdentificationTypeId = userDto.IdentificationTypeId,
                    IdentificationNumber = userDto.IdentificationNumber,
                    FirstName = userDto.FirstName,
                    MiddleName = userDto.MiddleName,
                    FirstSurname = userDto.FirstSurname,
                    SecondLastName = userDto.SecondLastName,
                    FullName = userDto.FullName,
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Email = userDto.Email,
                    IsActive = userDto.IsActive
                };
                bool isExist = await ValidateExist(userDto.Username, userDto.IdentificationNumber, cancellation);
                if (saveData.Id == 0 && !isExist)
                {
                    saveData.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                    await _userRepository.CreateUser(saveData, cancellation);
                    result = await _userRepository.ValidateExist(saveData.Username, saveData.IdentificationNumber, cancellation);
                }
                else if (saveData.Id != 0)
                {
                    saveData.Password = userDto.Password.Length> 0 ? BCrypt.Net.BCrypt.HashPassword(userDto.Password) : string.Empty;
                    await _userRepository.UpdateUser(saveData, cancellation);
                    result = await _userRepository.GetUserById(saveData.Id, cancellation);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en crear o editar usuario");
            }
            return result;
        }

        public async Task<LoginUserDto?> GetUser(string username, CancellationToken cancellation = default)
        {
            try
            {
                return await _userRepository.GetUser(username, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return null;
        }

        public async Task<GetUsersDto?> GetUserById(int id, CancellationToken cancellation = default)
        {
            GetUsersDto? userDto = null;
            try
            {
                userDto = await _userRepository.GetUserById(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return userDto;
        }

        public async Task<IEnumerable<GetUsersDto>> GetUsers(CancellationToken cancellation = default)
        {
            IEnumerable<GetUsersDto> userDto = [];
            try
            {
                userDto = await _userRepository.GetUsers(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return userDto;
        }

        public async Task<bool> UpdateUserToken(LoginUserDto user, CancellationToken cancellation = default)
        {
            try
            {
                return await _userRepository.UpdateUserToken(user, cancellation);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, Constants.GetUserError);
            }
            return false;
        }

        private async Task<bool> ValidateExist(string username, string numberIdentification, CancellationToken cancellation = default)
        {
            GetUsersDto? userDto = null;
            try
            {
                userDto = await _userRepository.ValidateExist(username, numberIdentification, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return userDto != null;
        }
    }
}
