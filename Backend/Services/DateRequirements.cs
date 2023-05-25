using Microsoft.AspNetCore.Authorization;

namespace Services
{
    public class DateRequirement : IAuthorizationRequirement
    {
        public DateTime[] Dates { get; }

        public DateRequirement(DateTime[] _)
        {
            Dates = _;
        }
    }
}
