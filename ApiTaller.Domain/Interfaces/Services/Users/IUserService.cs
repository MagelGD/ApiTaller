using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Users
{
    public interface IUserService
    {
        Task<User> GetUser(string username, CancellationToken cancellation = default!);
    }
}
