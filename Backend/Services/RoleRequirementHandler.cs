using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Services;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role)) return Task.CompletedTask;
        {
            var roles = context.User.FindAll(c => c.Type == "DocumentCheck").Select(c => c.Value);
            if (roles.Intersect(requirement.Roles).Any())
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}