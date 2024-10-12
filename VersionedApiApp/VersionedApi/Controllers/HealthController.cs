using Microsoft.AspNetCore.Mvc;

namespace VersionedApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiVersionNeutral]
public class HealthController : ControllerBase
{
    [HttpGet]
    [Route("ping")]
    public IActionResult Ping()
    {
        return Ok("Everything seems great!");
    }
}