using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userService.GetUsers(cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users");
            }
            return BadRequest();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userService.GetUserById(id, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id");
            }
            return BadRequest();
        }

        [HttpPost("SaveOrEditUsers")]
        public async Task<IActionResult> SaveOrEditUsers(GetUsersDto getUsersDto, CancellationToken cancellation)
        {
            try
            {
                return Ok(await _userService.CreateOrEditUser(getUsersDto, cancellation));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or editing user");
            }
            return BadRequest();
        }
    }
}
