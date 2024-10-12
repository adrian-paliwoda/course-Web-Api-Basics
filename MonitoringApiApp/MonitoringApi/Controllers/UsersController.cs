using Microsoft.AspNetCore.Mvc;

namespace MonitoringApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }
    
    // GET: api/<UsersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        throw new Exception("Something bad happened here.");
        // return new[] { "value1", "value2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            // if (id is < 0 or > 100)
            // {
            //     _logger.LogWarning("The given Id of {Identifier} was of the range", id);
            //     return BadRequest("The index was of the range");
            // }
            
            if (id is < 0 or > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            _logger.LogInformation("The api/User/{id} was called", id);
            return Ok($"Value{id}");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "The api/User/{id} ended unexpectedly", id);
            return BadRequest("Cannot process your request. Make sure you enter correct data");
        }
    }

    // POST api/<UsersController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

}