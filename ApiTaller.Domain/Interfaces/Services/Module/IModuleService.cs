using ApiTaller.Domain.Dtos.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Module
{
    public interface IModuleService
    {
        Task<IEnumerable<GetModule>> GetModules(CancellationToken cancellation = default!);
        Task<GetModule?> GetModuleById(int id, CancellationToken cancellation = default!);
        Task<GetModule> SaveOrEditModule(GetModule module, CancellationToken cancellation = default!);
    }
}
