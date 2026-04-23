using ApiTaller.Domain.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Login
{
    public interface ILoginService 
    {
        Task<bool> AddUserLogin(LoginUserDto user, CancellationToken cancellation = default!);
    }
}
