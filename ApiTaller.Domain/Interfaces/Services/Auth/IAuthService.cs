using ApiTaller.Domain.Dtos.Login;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<Income> Login(Domain.Dtos.Login.Auth auth, CancellationToken cancellation = default!);
    }
}
