using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ApiSecurity.Jwt;

public static class JwtOption
{
    public static void GetBearerOptions(JwtBearerOptions options, ConfigurationManager configuration)
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = configuration.GetValue<string>("Authentication:Audience"),
            ValidIssuer = configuration.GetValue<string>("Authentication:Issuer"),
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(configuration.GetValue<string>("Authentication:SecretKey") ??
                                            string.Empty))
        };
    }
}