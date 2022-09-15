using Microsoft.AspNetCore.Authorization;

namespace Bookshop.WebApp.Attributes
{
    public class RolesAuthorizeAttribute : AuthorizeAttribute
    {
        public RolesAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
