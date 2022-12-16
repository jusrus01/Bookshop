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

            return GetUserId(user);
        }

        public static bool IsAdminOrManager(this ClaimsPrincipal user) =>
            user.IsAdmin() || user.IsManager();

        public static bool IsSelf(this ClaimsPrincipal user, string id) => GetUserId(user) == id;

        public static bool IsAdmin(this ClaimsPrincipal user) =>
            user.IsInRole(BookshopRoles.Administrator);

        public static bool IsManager(this ClaimsPrincipal user) =>
            user.IsInRole(BookshopRoles.Manager);

        public static bool IsAdminOrOwner(this ClaimsPrincipal user, string userId) =>
            user.IsInRole(BookshopRoles.Administrator) || user.GetAuthenticatedUserId() == userId;

        private static string GetUserId(ClaimsPrincipal user) => 
            user.FindFirstValue(ClaimTypes.NameIdentifier);

        private static bool IsAuthenticated(ClaimsPrincipal user) =>
            user != null &&
            user.Identity != null &&
            user.Identity.IsAuthenticated;
    }
}
