using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Todo.Model;

namespace MinimalApi.Endpoints;

public static class AuthenticationEndpoints
{
    public record AuthenticationDate(string? UserName, string? Password);
    public record UserData(int Id, string FirstNam, string LastName, string UserName);
    
    public static void AddAuthentication(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/authentication/token", Authenticate);
    }
    
    public static ActionResult<string> Authenticate(IConfiguration configuration, [FromBody]AuthenticationDate? data)
    {
        var user = ValidateCredentials(data);
        if (user is null)
        {
            return new UnauthorizedResult();
        }
        
        var token = GenerateToken(user, configuration);

        return token;
    }

    private static UserData? ValidateCredentials(AuthenticationDate? data)
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
    
    private static string GenerateToken(UserData data, IConfiguration configuration)
    {
        var securityKey =
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, data.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, data.UserName),
            new(JwtRegisteredClaimNames.GivenName, data.FirstNam),
            new(JwtRegisteredClaimNames.FamilyName, data.LastName)
        };

        var token = new JwtSecurityToken(
            configuration.GetValue<string>("Authentication:Issuer"),
            configuration.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.Now.AddMinutes(1),
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private static bool CompareValues(string? actual, string expected)
    {
        if (actual is not null && actual.Equals(expected))
        {
            return true;
        }

        return false;
    }
    

}