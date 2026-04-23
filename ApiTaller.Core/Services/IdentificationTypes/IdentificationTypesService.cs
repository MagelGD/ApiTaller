using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Repositories.IdentificationTypes;
using ApiTaller.Domain.Interfaces.Services.IdentificationTypes;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Core.Services.IdentificationTypes
{
    public class IdentificationTypesService : IIdentificationTypesService
    {
        private readonly IIdentificationTypesRepository _repository;
        private readonly ILogger<IdentificationTypesService> _logger;
        public IdentificationTypesService(IIdentificationTypesRepository repository, ILogger<IdentificationTypesService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<GetIdentificationTypeDto> CreateOrEditIdentificationType(GetIdentificationTypeDto createDto, CancellationToken cancellation)
        {
            GetIdentificationTypeDto result = new();
            try
            {
                IdentificationType saveData = new()
                {
                    Id = createDto.Id,
                    Identification = createDto.Name,
                    IsActive = createDto.IsActive,
                    CreatedAt = createDto.CreatedAt ?? DateTime.Now,
                };
                bool isExist = await ValidateExist(createDto.Name, cancellation);
                if (saveData.Id == 0 && !isExist)
                {
                    await _repository.CreateAsync(saveData, cancellation);
                    result = await _repository.GetByNameAsync(createDto.Name, cancellation) ?? new();
                }
                else if (saveData.Id != 0)
                {
                    await _repository.UpdateAsync(saveData, cancellation);
                    result = await _repository.GetByIdAsync(saveData.Id, cancellation);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or editing identification type");
            }
            return result;
        }

        public async Task<IEnumerable<GetIdentificationTypeDto>> GetAllAsync(CancellationToken cancellation)
        {
            IEnumerable<GetIdentificationTypeDto> result = [];
            try
            {
                result = await _repository.GetAllAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all active identification types");
            }
            return result;
        }

        public async Task<IEnumerable<GetIdentificationTypeDto>> GetAllActiveAsync(CancellationToken cancellation)
        {
            IEnumerable<GetIdentificationTypeDto> result = [];
            try
            {
                result = await _repository.GetAllActiveAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all active identification types");
            }
            return result;
        }

        public async Task<GetIdentificationTypeDto?> GetByIdAsync(int id, CancellationToken cancellation)
        {
            GetIdentificationTypeDto? result = null;
            try
            {
                result = await _repository.GetByIdAsync(id, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting identification type with id {id}");
            }
            return result;
        }

        private async Task<bool> ValidateExist(string name, CancellationToken cancellation)
        {
            bool result = false;
            try
            {
                result = await _repository.ValidateExist(name, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating existence of identification type with name {name}");
            }
            return result;
        }
    }
}

