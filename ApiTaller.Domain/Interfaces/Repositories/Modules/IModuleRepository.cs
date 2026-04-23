using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Repositories.Modules
{
    public interface IModuleRepository
    {
        Task<IEnumerable<GetModuleDto>> GetModules(CancellationToken cancellation = default!);
        Task<GetModuleDto?> GetModuleById(int id, CancellationToken cancellation = default!);
        Task<GetModuleDto?> GetModuleName(string Module, CancellationToken cancellation = default!);
        Task<bool> SaveModule(Module module, CancellationToken cancellation = default!);
        Task<bool> UpdateModule(Module module, CancellationToken cancellation = default!);
    }
}
