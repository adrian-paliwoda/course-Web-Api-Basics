using Microsoft.AspNetCore.Mvc;

namespace SwaggerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings]
public class UsersController : ControllerBase
{
    /// <summary>
    /// Get a list of all users in the system
    /// </summary>
    /// <remark>
    /// Sample Request: GET /Users
    /// Sample Response:
    /// [
    ///     {
    ///         "id": 1,
    ///         "name": "user 1"
    ///     },
    ///     {
    ///         "id": 2,
    ///         "name": "Adrian"
    ///     }
    /// ]
    /// </remark>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    [HttpPut]
    public void Put(int id, [FromBody] string value)
    {
        
    }

    [HttpDelete]
    public void Delete(int id)
    {
        
    }
}