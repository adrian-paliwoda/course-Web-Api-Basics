using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(IConfiguration configuration, ILogger<AuthenticationController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public record AuthenticationDate(string? UserName, string? Password);
    public record UserData(int Id, string FirstNam, string LastName, string UserName);

    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody]AuthenticationDate? data)
    {
        _logger.LogInformation("Authentication started");
        var user = ValidateCredentials(data);
        if (user is null)
        {
            _logger.LogError("Authentication failed");
            return new UnauthorizedResult();
        }

        _logger.LogInformation("Authentication completed successfully");
        var token = GenerateToken(user);

        return token;
    }

    [NonAction]
    private UserData? ValidateCredentials(AuthenticationDate? data)
    {
        // THIS IS NOT PRODUCTION CODE - REPLACE THIS WITH CALL TO YOUR AUTH SYSTEM

        if (data is not null && CompareValues(data.UserName, "adrian") && CompareValues(data.Password, "test123"))
        {
            return new UserData(1, "Adrian", "Paliwoda", data.UserName);
        }
        
        if (data is not null && CompareValues(data.UserName, "sstorm") && CompareValues(data.Password, "test123"))
        {
            return new UserData(1, "Adrian", "Paliwoda", data.UserName);
        }

        return null;
    }
    
    [NonAction]
    private string GenerateToken(UserData data)
    {
        var securityKey =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, data.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, data.UserName),
            new(JwtRegisteredClaimNames.GivenName, data.FirstNam),
            new(JwtRegisteredClaimNames.FamilyName, data.LastName)
        };

        var token = new JwtSecurityToken(
            _configuration.GetValue<string>("Authentication:Issuer"),
            _configuration.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.Now.AddMinutes(1),
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    [NonAction]
    private bool CompareValues(string? actual, string expected)
    {
        if (actual is not null && actual.Equals(expected))
        {
            return true;
        }

        return false;
    }
    

}