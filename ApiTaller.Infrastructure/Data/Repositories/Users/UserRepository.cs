using ApiTaller.Domain.Common.Constants;
using ApiTaller.Domain.Dtos.IdentificationTypes;
using ApiTaller.Domain.Dtos.UserRole;
using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Repositories.Users;
using ApiTaller.Domain.Interfaces.Services;
using ApiTaller.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiTaller.Infrastructure.Data.Repositories.Users
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly ICurrentUserService _currentUserService;
        public UserRepository(DataContext dataContext, ILogger<UserRepository> logger, ICurrentUserService currentUserService)
        {
            _context = dataContext;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<bool> CreateUser(User user, CancellationToken cancellation = default)
        {
            try
            {
                if (_context.CurrentTenantId > 0 && user.WorkshopId == null)
                {
                    user.WorkshopId = _context.CurrentTenantId;
                }

                await _context.User.AddAsync(user, cancellation);
                return await _context.SaveChangesAsync(cancellation) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
            }   
            return false;
        }

        public async Task<LoginUserDto?> GetUser(string username, CancellationToken cancellation = default!)
        {
            try
            {
                LoginUserDto? Query = await _context.User.Include(x => x.WorkshopNavigation).Select(x => new LoginUserDto
                {
                    Id = x.Id,
                    UserName = x.Username,
                    Password = x.Password,
                    Fullname = x.FullName,
                    Token = x.Token,
                    IdUserRole = x.UserRoleId,
                    WorkshopId = x.WorkshopId,
                    WorkshopType = x.WorkshopNavigation != null ? x.WorkshopNavigation.WorkshopType : "moto",
                    WorkshopName = x.WorkshopNavigation != null ? x.WorkshopNavigation.Name : null
                }).FirstOrDefaultAsync(x => x.UserName == username, cancellation);
                return Query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return null;
        }

        public async Task<GetUsersDto?> GetUserById(int id, CancellationToken cancellation = default)
        {
            GetUsersDto? Query = null;
            try
            {
                Query = await _context.User.Include(x => x.UserRoleIdNavigation).Include(x => x.IdentificationTypeIdNavigation).Where(x => x.Id == id).Select(x => new GetUsersDto
                {
                    Id = x.Id,
                    UserRoleId = x.UserRoleId,
                    IdentificationTypeId = x.IdentificationTypeId,
                    IdentificationNumber = x.IdentificationNumber,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    FirstSurname = x.FirstSurname,
                    SecondLastName = x.SecondLastName,
                    FullName = x.FullName,
                    Username = x.Username,
                    Password = x.Password,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    UserRoleDto = new GetUserRoleDto
                    {
                        IdUserRol = x.UserRoleIdNavigation.Id,
                        RoleName = x.UserRoleIdNavigation.Role,
                        IsActive = x.UserRoleIdNavigation.IsActive
                    },
                    IdentificationTypeDto = new GetIdentificationTypeDto
                    {
                        Id = x.IdentificationTypeIdNavigation.Id,
                        Name = x.IdentificationTypeIdNavigation.Identification,
                        IsActive = x.IdentificationTypeIdNavigation.IsActive
                    }
                }).FirstOrDefaultAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, Constants.GetUserError);
            }
            return Query;
        }

        public async Task<IEnumerable<GetUsersDto>> GetUsers(CancellationToken cancellation = default)
        {
            IEnumerable<GetUsersDto> Query = [];
            try
            {
                Query = await _context.User.Include(x => x.UserRoleIdNavigation).Include(x => x.IdentificationTypeIdNavigation).Select(x => new GetUsersDto
                {
                    Id = x.Id,
                    UserRoleId = x.UserRoleId,
                    IdentificationTypeId = x.IdentificationTypeId,
                    IdentificationNumber = x.IdentificationNumber,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    FirstSurname = x.FirstSurname,
                    SecondLastName = x.SecondLastName,
                    FullName = x.FullName,
                    Username = x.Username,
                    Password = x.Password,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    UserRoleDto = new GetUserRoleDto
                    {
                        IdUserRol = x.UserRoleIdNavigation.Id,
                        RoleName = x.UserRoleIdNavigation.Role,
                        IsActive = x.UserRoleIdNavigation.IsActive
                    },
                    IdentificationTypeDto = new GetIdentificationTypeDto
                    {
                        Id = x.IdentificationTypeIdNavigation.Id,
                        Name = x.IdentificationTypeIdNavigation.Identification,
                        IsActive = x.IdentificationTypeIdNavigation.IsActive
                    }
                }).ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return Query;
        }

        public async Task<bool> UpdateUser(User user, CancellationToken cancellation = default)
        {
            try
            {
                int rows = await _context.User.Where(x => x.Id == user.Id).ExecuteUpdateAsync(x => x
                    .SetProperty(p => p.UserRoleId, user.UserRoleId)
                    .SetProperty(p => p.IdentificationTypeId, user.IdentificationTypeId)
                    .SetProperty(p => p.IdentificationNumber, user.IdentificationNumber)
                    .SetProperty(p => p.FirstName, user.FirstName)
                    .SetProperty(p => p.MiddleName, user.MiddleName)
                    .SetProperty(p => p.FirstSurname, user.FirstSurname)
                    .SetProperty(p => p.SecondLastName, user.SecondLastName)
                    .SetProperty(p => p.FullName, user.FullName)
                    .SetProperty(p => p.Username, user.Username)
                    .SetProperty(p => p.Password, user.Password)
                    .SetProperty(p => p.Email, user.Email)
                    .SetProperty(p => p.IsActive, user.IsActive)
                    .SetProperty(p => p.UpdatedAt, DateTime.Now), cancellation);
                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, Constants.GetUserError);
            }
            return false;
        }

        public async Task<bool> UpdateUserToken(LoginUserDto user, CancellationToken cancellation = default!)
        {
            try
            {
                int rows = await _context.User
                    .Where(x => x.Id == user.Id)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(p => p.Token, user.Token)
                        .SetProperty(p=> p.CreatedAt, DateTime.Now)
                        .SetProperty(p=> p.AssignmentDate, DateTime.Now)
                        .SetProperty(p=> p.ExpirationDate, DateTime.Now.AddHours(user.ExpireToken ?? 0))
                        .SetProperty(p => p.UpdatedAt, DateTime.Now), cancellation);
                return rows > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return false;
        }

        public async Task<GetUsersDto?> ValidateExist(string username, string numberIdentification, CancellationToken cancellation = default)
        {
            GetUsersDto? result = null;
            try
            {
                result = _context.User.Where(x => x.Username == username || x.IdentificationNumber == numberIdentification).Select(x => new GetUsersDto   
                {
                    Id = x.Id,
                    UserRoleId = x.UserRoleId,
                    IdentificationTypeId = x.IdentificationTypeId,
                    IdentificationNumber = x.IdentificationNumber,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    FirstSurname = x.FirstSurname,
                    SecondLastName = x.SecondLastName,
                    FullName = x.FullName,
                    Username = x.Username,
                    Password = x.Password,
                    Email = x.Email,
                    IsActive = x.IsActive
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, Constants.GetUserError);
            }
            return result;
        }
    }
}
