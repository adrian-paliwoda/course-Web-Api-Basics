﻿using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    // GET: api/v1/<UsersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "VersionValue1", "VersionValue2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
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