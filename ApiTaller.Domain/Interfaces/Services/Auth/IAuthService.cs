using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> Login(string username, string password, CancellationToken cancellation = default!);
    }
}
