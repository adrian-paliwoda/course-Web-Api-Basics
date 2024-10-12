using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiSecurity.Controllers;


[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private const string AuthenticationIssuerKey = "Authentication:Issuer";
    private const string AuthenticationAudienceKey = "Authentication:Audience";
    private const string AuthenticationSecretKey = "Authentication:SecretKey";
    
    private readonly IConfiguration _configuration;

    public record AuthenticationData(string? Username, string? Password);
    public record UserData(int UserId, string UserName, string Title, string EmployeeId);

    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpPost("token")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var userData = ValidateCredentials(data);
        if (userData is not null)
        {
            var token = GenerateToken(userData);
         
            return Ok(token);
        }
        
        return Unauthorized(userData);
    }
    
    [HttpPost("tokenTest")]
    public ActionResult<string> AuthenticateToken([FromBody] string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _configuration.GetValue<string>(AuthenticationAudienceKey),
                ValidIssuer = _configuration.GetValue<string>(AuthenticationIssuerKey),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>(AuthenticationSecretKey) ?? string.Empty)),
            };


            var claims = handler.ValidateToken(token, tokenValidationParameters, out _);
            if (claims.HasClaim((c) => c.Value.Equals(JwtRegisteredClaimNames.UniqueName)))
            {
                return Ok(token);
            }
        }
        
        return Unauthorized();
    }
    
    [HttpPost("users")]
    public ActionResult<string> Users()
    {
        return "Ok(?)";
    }

    private string GenerateToken(UserData userData)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                _configuration.GetValue<string>(AuthenticationSecretKey)!));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>();
        claims.Add(new(JwtRegisteredClaimNames.Sub, userData.UserId.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, userData.UserName));
        claims.Add(new Claim("title", userData.Title));
        claims.Add(new Claim("employeeId", userData.EmployeeId));

        var token = new JwtSecurityToken(
            _configuration.GetValue<string>(AuthenticationIssuerKey),
            _configuration.GetValue<string>(AuthenticationAudienceKey),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(2),
            signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        if (CompareValues(data.Username, "tcorey")
            && CompareValues(data.Password, "Test123"))
        {
            return new UserData(1, data.Username!, "Business Owner", "E001");
        }
        
        if (CompareValues(data.Username, "zuz")
            && CompareValues(data.Password, "Test123"))
        {
            return new UserData(2, data.Username!, "Senior Developer", "E002");
        }
        
        if (CompareValues(data.Username, "sstorm")
            && CompareValues(data.Password, "Test123"))
        {
            return new UserData(2, data.Username!, "Junior Developer", "E005");
        }
        
        return null;
    }

    private bool CompareValues(string? actual, string expected)
    {
        return actual is not null 
               && actual.Equals(expected, StringComparison.InvariantCultureIgnoreCase);
    }
}