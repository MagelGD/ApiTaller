using ApiTaller.Domain.Dtos.Module;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Domain.Interfaces.Services.Module
{
    public interface IModuleService
    {
        Task<IEnumerable<GetModuleDto>> GetModules(CancellationToken cancellation = default!);
        Task<GetModuleDto?> GetModuleById(int id, CancellationToken cancellation = default!);
        Task<GetModuleDto> SaveOrEditModule(GetModuleDto module, CancellationToken cancellation = default!);
    }
}
