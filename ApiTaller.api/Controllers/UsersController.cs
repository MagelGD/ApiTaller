using ApiTaller.Domain.Dtos.Users;
using ApiTaller.Domain.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userService.GetUsers(cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting users");
            }
            return BadRequest();
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userService.GetUserById(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by id");
            }
            return BadRequest();

        }

        // POST api/<UsersController>
        [HttpPost("SaveOrEditUsers")]
        public async Task<IActionResult> Post(GetUsersDto getUsersDto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _userService.CreateOrEditUser(getUsersDto, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving or editing user");
            }
            return BadRequest();
        }

        //// PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
