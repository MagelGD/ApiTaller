using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Interfaces.Repositories.UserRoleModules;
using ApiTaller.Domain.Interfaces.Services.UserRoleModules;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.UserRoleModules
{
    public class UserRoleModuleService : IUserRoleModuleService
    {
        private readonly IUserRoleModuleRepository _userRoleModuleRepository;
        private readonly ILogger<UserRoleModuleService> _logger;

        public UserRoleModuleService(IUserRoleModuleRepository userRoleModuleRepository, ILogger<UserRoleModuleService> logger)
        {
            _userRoleModuleRepository = userRoleModuleRepository;
            _logger = logger;
        }

        public async Task<GetUserRoleModule?> GetUserRoleModuleById(int id, CancellationToken cancellation = default)
        {
            try
            {
                GetUserRoleModule? userRoleModule = await _userRoleModuleRepository.GetUserRoleModuleById(id, cancellation);
                return userRoleModule;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el UserRoleModule con ID {Id}", id);
            }
            return null;
        }

        public async Task<IEnumerable<GetUserRoleModule>> GetUserRoleModules(CancellationToken cancellation = default)
        {
            IEnumerable<GetUserRoleModule> userRoleModules = [];
            try
            {
                userRoleModules = await _userRoleModuleRepository.GetUserRoleModules(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erroral obtener todo los urserrolemodule");
            }
            return userRoleModules;
        }

        public async Task<GetUserRoleModule> SaveOrEditUserRoleModule(GetUserRoleModule userRoleModule, CancellationToken cancellation = default)
        {
            GetUserRoleModule data = new();
            try
            {
                UserRoleModule saveData = new()
                {
                    Id = userRoleModule.id,
                    UserRoleId = userRoleModule.Role.IdUserRol,
                    ModulesRoleId = userRoleModule.Module.Id,
                    IsActive = userRoleModule.IsActive,
                    CreatedAt = userRoleModule.CreatedAt ?? DateTime.Now
                };
                bool isExist = await _userRoleModuleRepository.ValidateExistUserRoleModule(userRoleModule.Role.IdUserRol, userRoleModule.Module.Id ,cancellation);
                if(saveData.Id == 0 && !isExist)
                {
                    await _userRoleModuleRepository.SaveUserRoleModule(saveData, cancellation);
                }
                else if(saveData.Id != 0)
                {
                    await _userRoleModuleRepository.UpdateUserRoleModule(saveData, cancellation);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el UserRoleModule con ID {Id}", userRoleModule.id);
            }
            return data;
        }
    }
}
