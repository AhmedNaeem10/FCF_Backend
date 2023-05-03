using Microsoft.AspNetCore.Mvc.Filters;
using FCF.Helpers.Dtos;

namespace FCF.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (UserDto)context.HttpContext.Items["User"];
            if (user == null || !Role.Split(",").Contains(user.role))
            {
                throw new ApplicationException("Failed to authorize the api caller!");
            }
        }
    }
}
