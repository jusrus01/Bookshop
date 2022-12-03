using Bookshop.Contracts.Constants;
using System.Security.Claims;

namespace Bookshop.BusinessLogic.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <param name="user">Logged in user</param>
        /// <returns>User id or null</returns>
        public static string? GetAuthenticatedUserId(this ClaimsPrincipal user)
        {
            if (!IsAuthenticated(user))
            {
                return null;
            }
            
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdminOrOwner(this ClaimsPrincipal user, string userId) =>
            user.IsInRole(BookshopRoles.Administrator) ||user.GetAuthenticatedUserId() == userId;

        private static bool IsAuthenticated(ClaimsPrincipal user) =>
            user != null &&
            user.Identity != null &&
            user.Identity.IsAuthenticated;
    }
}
