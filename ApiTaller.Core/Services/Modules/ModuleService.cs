using ApiTaller.Domain.Dtos.Module;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Services.Module;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.Modules
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ILogger<ModuleService> _logger;
        public ModuleService(IModuleRepository moduleRepository, ILogger<ModuleService> logger)
        {
            _logger = logger;
            _moduleRepository = moduleRepository;
        }
        public async Task<GetModule?> GetModuleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetModule? module = await _moduleRepository.GetModuleById(id, cancellation);
                return module;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el módulo con id {Id}", id);
            }
            return null;
        }

        public async Task<IEnumerable<GetModule>> GetModules(CancellationToken cancellation = default)
        {
            IEnumerable<GetModule> modules = [];
            try
            {
                modules = await _moduleRepository.GetModules(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los módulos");
            }
            return modules;
        }

        public async Task<GetModule> SaveOrEditModule(GetModule module, CancellationToken cancellation = default)
        {
            GetModule data = new();
            try
            {
                Module saveData = new()
                {
                    Id = module.Id,
                    Name = module.Name,
                    IsActive = module.IsActive,
                    CreatedAt = module.CreatedAt ?? DateTime.Now
                };
                bool isExist = await ModuleValidation(module.Name, cancellation);
                if (saveData.Id == 0 && !isExist)
                {
                    data.CreatedAt = DateTime.Now;
                    await _moduleRepository.SaveModule(saveData, cancellation);
                }
                else
                {
                    data.UpdatedAt = DateTime.Now;
                    await _moduleRepository.UpdateModule(saveData, cancellation);
                }
                data = await _moduleRepository.GetModuleName(saveData.Name, cancellation) ?? new();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el módulo con id {Id}", module.Id);
            }
            return data;
        }

        private async Task<bool> ModuleValidation(string name, CancellationToken cancellation = default)
        {
            try
            {
                GetModule? module = await _moduleRepository.GetModuleName(name, cancellation);
                return module != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al validar el módulo con nombre {Name}", name);
            }
            return false;
        }
    }
}
