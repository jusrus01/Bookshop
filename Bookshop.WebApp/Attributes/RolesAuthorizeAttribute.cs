using Microsoft.AspNetCore.Authorization;

namespace Bookshop.WebApp.Attributes
{
    public class AuthorizeAnyOfTheRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeAnyOfTheRolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
