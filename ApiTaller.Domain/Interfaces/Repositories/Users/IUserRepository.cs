using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<GetUser?> GetUser(string username, CancellationToken cancellation = default!);
        Task<bool> UpdateUserToken(GetUser user, CancellationToken cancellation = default!); 
    }
}
