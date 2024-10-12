using Microsoft.AspNetCore.Authorization;

namespace ApiSecurity.Policy;

public static class PolicyOption
{
    public static void Get(AuthorizationOptions options)
    {
        AddPolicies(options);

        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    }

    private static void AddPolicies(AuthorizationOptions options)
    {
        options.AddPolicy(PolicyConstants.MustHaveEmployeeId, policy => { policy.RequireClaim("employeeId"); });
        options.AddPolicy(PolicyConstants.MustBeTheOwner,
            policy => { policy.RequireClaim("title", "Business Owner"); });
        options.AddPolicy(PolicyConstants.MustBeVeteranEmployee,
            policy => { policy.RequireClaim("employeeId", "E001", "E002"); });
        
        options.AddPolicy(PolicyConstants.AllUserData, builder =>
        {
            builder.RequireAssertion(context =>
                context.User
                    .HasClaim("title", "Business Owner")
                || context.User.HasClaim(p => p.Type.Equals("employeeId") && p.Value.Contains("E002")));
        });
    }
}