using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<LoginUserDto?> GetUser(string username, CancellationToken cancellation = default!);
        Task<bool> UpdateUserToken(LoginUserDto user, CancellationToken cancellation = default!); 
        Task<IEnumerable<GetUsersDto>> GetUsers(CancellationToken cancellation = default!);
        Task<GetUsersDto?> GetUserById(int id, CancellationToken cancellation = default!);
        Task<bool> CreateUser(User user, CancellationToken cancellation = default!);
        Task<bool> UpdateUser(User user, CancellationToken cancellation = default!);
        Task<GetUsersDto?> ValidateExist(string username, string numberIdentification, CancellationToken cancellation = default!);
    }
}
