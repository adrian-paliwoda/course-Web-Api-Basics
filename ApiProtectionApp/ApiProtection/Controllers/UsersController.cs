using ApiProtection.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiProtection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/<UsersController>
    [HttpGet]
    //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public IEnumerable<string> Get()
    {
        return new[] { Random.Shared.Next(1, 101).ToString() };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public string Get(int id)
    {
        return $"Random Number is: {Random.Shared.Next(1, 101)} for id: {id}";
    }

    // POST api/<UsersController>
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        if (ModelState.IsValid)
        {
            return Ok("The model was valid");
        }

        return BadRequest(ModelState);
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        // Only for demo
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        // Only for demo
    }
}