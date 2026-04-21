using ApiTaller.Domain.Dtos.Action;
using ApiTaller.Domain.Dtos.RoleActions;
using ApiTaller.Domain.Dtos.UserRoleModule;
using ApiTaller.Domain.Interfaces.Repositories.Actions;
using ApiTaller.Domain.Interfaces.Repositories.Modules;
using ApiTaller.Domain.Interfaces.Repositories.RoleActions;
using ApiTaller.Domain.Interfaces.Repositories.UserRoleModules;
using ApiTaller.Domain.Interfaces.Repositories.UserRoles;
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
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IModuleRepository _ModuleRepository;
        private readonly IRoleActionsRepository _roleActionsRepository;
        private readonly IActionRepository _actionRepository;
        private readonly ILogger<UserRoleModuleService> _logger;

        public UserRoleModuleService(IUserRoleModuleRepository userRoleModuleRepository, ILogger<UserRoleModuleService> logger,
            IUserRoleRepository userRoleRepository, IModuleRepository moduleRepository, IRoleActionsRepository roleActionsRepository, IActionRepository actionRepository)
        {
            _userRoleModuleRepository = userRoleModuleRepository;
            _userRoleRepository = userRoleRepository;
            _ModuleRepository = moduleRepository;
            _logger = logger;
            _roleActionsRepository = roleActionsRepository;
            _actionRepository = actionRepository;
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

        public async Task<GetUserRoleModule> SaveOrEditUserRoleModule(SaveUserRoleModule saveUserRoleModule, CancellationToken cancellation = default)
        {
            GetUserRoleModule userRoleModule = new();
            try
            {
                userRoleModule.Role = await _userRoleRepository.GetUserRoleById(saveUserRoleModule.userRoleId, cancellation) ?? new();
                userRoleModule.Module = await _ModuleRepository.GetModuleById(saveUserRoleModule.modulesRoleId, cancellation) ?? new();
                userRoleModule.IsActive = saveUserRoleModule.isActive;
                UserRoleModule saveData = new()
                {
                    Id = userRoleModule.id,
                    UserRoleId = userRoleModule.Role.IdUserRol,
                    ModulesRoleId = userRoleModule.Module.Id,
                    IsActive = userRoleModule.IsActive,
                    CreatedAt = userRoleModule.CreatedAt ?? DateTime.Now
                };
                bool isExist = await _userRoleModuleRepository.ValidateExistUserRoleModule(userRoleModule.Role.IdUserRol, userRoleModule.Module.Id, cancellation);
                if (saveData.Id == 0 && !isExist)
                {
                    await _userRoleModuleRepository.SaveUserRoleModule(saveData, cancellation);
                }
                else if (saveData.Id != 0)
                {
                    await _userRoleModuleRepository.UpdateUserRoleModule(saveData, cancellation);
                }
                userRoleModule = await _userRoleModuleRepository.GetuserRoleModulesCreate(saveData.UserRoleId, saveData.ModulesRoleId, cancellation) ?? new();
                await InsertActions(saveUserRoleModule.actions, saveUserRoleModule.userRoleId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar o editar el UserRoleModule con ID {Id}", userRoleModule.id);
            }
            return userRoleModule;
        }

        private async Task<bool> InsertActions(List<ActionsRole> actionsIds, int userRoleModuleId, CancellationToken cancellation)
        {
            try
            {
                foreach (ActionsRole actionId in actionsIds)
                {
                    int existingActionId = await ValidateExist(userRoleModuleId, actionId.ActionId, cancellation);
                    if (existingActionId != 0)
                    {
                        await UpdateActions(existingActionId, actionId, userRoleModuleId, cancellation);
                        continue;
                    }
                    await _roleActionsRepository.SaveRoleAction(new RoleAction
                    {
                        RoleId = userRoleModuleId,
                        ActionId = actionId.ActionId,
                        IsActive = actionId.IsActive,
                    }, cancellation);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar las acciones para el UserRoleModule con ID {Id}", userRoleModuleId);
                return false;
            }
        }

        private async Task<int> ValidateExist(int id,int idaction, CancellationToken cancellationToken)
        {
            try
            {
                List<ValidateRolAction> Query = await _roleActionsRepository.ValidateActionRoleAsync(id, cancellationToken);
                return Query.Where(x=> x.ActionId == idaction).Select(x=> x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
            return 0;
        }

        private async Task<bool> UpdateActions(int idRoleAction,ActionsRole actionsIds, int userRoleModuleId, CancellationToken cancellation)
        {
            try
            {
                await _roleActionsRepository.ActiveOrInactiveRoleAction(new RoleAction
                {
                    Id = idRoleAction,
                    RoleId = userRoleModuleId,
                    ActionId = actionsIds.ActionId,
                    IsActive = actionsIds.IsActive,
                }, cancellation);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar las acciones para el UserRoleModule con ID {Id}", userRoleModuleId);
                return false;
            }
        }
    }
}
