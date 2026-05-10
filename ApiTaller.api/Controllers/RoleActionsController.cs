using ApiTaller.Domain.Interfaces.Services.RoleActions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTaller.api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [Authorize]
    public class RoleActionsController : ControllerBase
    {

        private readonly IRoleActionService _roleActionService;
        private readonly ILogger<RoleActionsController> _logger;

        public RoleActionsController(IRoleActionService roleActionService, ILogger<RoleActionsController> logger)
        {
            _roleActionService = roleActionService;
            _logger = logger;
        }
        // GET: api/<RoleActionsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<RoleActionsController>/5
        [HttpGet("GetRoleActions/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _roleActionService.GetActionsByRoleIdAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando las acciones del rol");
            }
            return BadRequest();
        }

        [HttpGet("PermissionRole/{id}")]
        public async Task<IActionResult> RoleActions(int id, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _roleActionService.GetActionsByRoleAsync(id, cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando las acciones del rol");
            }
            return BadRequest();
        }

        //// POST api/<RoleActionsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RoleActionsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RoleActionsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
