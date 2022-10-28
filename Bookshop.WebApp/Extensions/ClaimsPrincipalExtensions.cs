using System.Security.Claims;

namespace Bookshop.WebApp.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <param name="user">Logged in user</param>
        /// <returns>User id or null</returns>
        public static string GetAuthenticatedUserId(this ClaimsPrincipal user)
        {

            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
