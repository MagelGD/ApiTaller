using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string username, CancellationToken cancellation = default!);
        Task save();
    }
}
