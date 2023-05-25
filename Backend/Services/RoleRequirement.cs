using Microsoft.AspNetCore.Authorization;

namespace Services
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; }

        public RoleRequirement(string[] roles)
        {
            Roles = roles;
        }
    }
}
