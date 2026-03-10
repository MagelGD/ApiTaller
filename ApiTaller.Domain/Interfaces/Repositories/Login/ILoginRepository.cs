using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Login
{
    public interface ILoginRepository
    {
        Task<bool> AddUserLogin(Models.Login login, CancellationToken cancellation = default!);
    }
}
