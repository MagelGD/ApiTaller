using ApiTaller.Domain.Dtos.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Repositories.BrandModelVersion;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.BrandModelVersions
{
    public class BrandModelVersionRepository : IBrandModelVersionRepository
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly DataContext _Context;
        private readonly ILogger<BrandModelVersionRepository> _logger;
        public BrandModelVersionRepository(ICurrentUserService currentUserService, DataContext dataContext, ILogger<BrandModelVersionRepository> logger)
        {
            _Context = dataContext;
            _currentUserService = currentUserService;
            _logger = logger;
        }
        public async Task<bool> CreateBrandModelVersionAsync(BrandModelVersion brandModelVersion, CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        public async Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionActiveAsync(CancellationToken cancellationToken)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        public Task<IEnumerable<GetBrandModelVersionDto>> GetBrandModelVersionAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<GetBrandModelVersionDto?> GetBrandModelVersionByIdAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBrandModelVersionAsync(BrandModelVersion brandModelVersion, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<GetBrandModelVersionDto?> ValidateExist(GetBrandModelVersionDto getBrandModelVersion, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
