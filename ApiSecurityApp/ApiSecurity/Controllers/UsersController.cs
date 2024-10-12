using ApiSecurity.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiSecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;

    public UsersController(IConfiguration config)
    {
        _config = config;
    }

    // GET: api/<UsersController>
    [HttpGet]
    [Authorize(Policy = PolicyConstants.AllUserData)]
    public IEnumerable<string> Get()
    {
        return new[] { "value1", "value2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    [Authorize(Policy = PolicyConstants.MustHaveEmployeeId)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        var user = User.Claims.FirstOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (user is not null && user.Value == id.ToString())
        {
            var result = _config.GetConnectionString("Default") ?? string.Empty;
            return Ok(result);
        }
        
        return Unauthorized("You have no access");
    }

    // POST api/<UsersController>
    [HttpPost]
    [Authorize(Policy = PolicyConstants.MustBeVeteranEmployee)]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    [Authorize(Policy = PolicyConstants.MustBeVeteranEmployee)]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    [Authorize(Policy = PolicyConstants.MustBeVeteranEmployee)]
    [Authorize(Policy = PolicyConstants.MustBeTheOwner)]
    public void Delete(int id)
    {
    }

}