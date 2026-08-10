using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Repositories.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.IdentificationTypes
{
    public class IdentificationTypesRepository : IIdentificationTypesRepository
    {
        private readonly DataContext _Context;
        private readonly ILogger<IdentificationTypesRepository> _Logger;
        private readonly ICurrentUserService _CurrentUserService;

        public IdentificationTypesRepository(DataContext context, ILogger<IdentificationTypesRepository> logger, ICurrentUserService currentUserService)
        {
            _Context = context;
            _Logger = logger;
            _CurrentUserService = currentUserService;
        }
        public async Task<bool> CreateAsync(IdentificationType identificationType, CancellationToken cancellation)
        {
            try
            {
                if(int.TryParse(_CurrentUserService.UserId, out int userId))
                {
                    identificationType.ResponsibleUserId = userId;
                }
                await _Context.IdentificationType.AddAsync(identificationType, cancellation);
                return await _Context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error creating identification type");
            }
            return false;
        }

        public async Task<IEnumerable<GetIdentificationTypeDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetIdentificationTypeDto> result = [];
            try
            {
                result = await _Context.IdentificationType
                    .Where(it => it.IsActive)
                    .Select(it => new GetIdentificationTypeDto
                    {
                        Id = it.Id,
                        Name = it.Identification,
                        IsActive = it.IsActive,
                        CreatedAt = it.CreatedAt,
                        UpdatedAt = it.UpdatedAt

                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting active identification types");
            }
            return result;
        }

        public async Task<IEnumerable<GetIdentificationTypeDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetIdentificationTypeDto> result = [];
            try
            {
                result = await _Context.IdentificationType
                    .Select(it => new GetIdentificationTypeDto
                    {
                        Id = it.Id,
                        Name = it.Identification,
                        IsActive = it.IsActive,
                        CreatedAt = it.CreatedAt,
                        UpdatedAt = it.UpdatedAt

                    }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting active identification types");
            }
            return result;
        }

        public async Task<GetIdentificationTypeDto> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetIdentificationTypeDto? result = null;
            try
            {
                result = await _Context.IdentificationType
                    .Where(it => it.Id == id)
                    .Select(it => new GetIdentificationTypeDto
                    {
                        Id = it.Id,
                        Name = it.Identification,
                        IsActive = it.IsActive,
                        CreatedAt = it.CreatedAt,
                        UpdatedAt = it.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting identification type by id {Id}", id);
            }
            return result!;
        }

        public async Task<GetIdentificationTypeDto?> GetByNameAsync(string name, CancellationToken cancellation)
        {
            GetIdentificationTypeDto? result = null;
            try
            {
                return await _Context.IdentificationType
                    .Where(it => it.Identification == name)
                    .Select(it => new GetIdentificationTypeDto
                    {
                        Id = it.Id,
                        Name = it.Identification,
                        IsActive = it.IsActive,
                        CreatedAt = it.CreatedAt,
                        UpdatedAt = it.UpdatedAt
                    }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error getting identification type by name {Name}", name);
            }
            return result;
        }

        public async Task<bool> UpdateAsync(IdentificationType identificationType, CancellationToken cancellation)
        {
            try
            {
                if (int.TryParse(_CurrentUserService.UserId, out int userId))
                {
                    identificationType.ResponsibleUserId = userId;
                }
                identificationType.UpdatedAt = DateTime.Now;
                _Context.Update(identificationType);
                return await _Context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error updating identification type with id {Id}", identificationType.Id);
            }
            return false;
        }

        public async Task<bool> ValidateExist(string name, CancellationToken cancellation)
        {
            try
            {
                return await _Context.IdentificationType.AnyAsync(it => it.Identification == name, cancellation);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error validating existence of identification type with name {Name}", name);
            }
            return false;
        }
    }
}
