using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Bookshop.WebApp.PageModels
{
    [PageModel]
    public abstract class BookshopPageModel : PageModel
    {
        protected readonly INotyfService _notyfService;

        public virtual string ErrorMessage { get; set; }

        public virtual bool HasError
        {
            get => ErrorMessage != null;
        }

        public BookshopPageModel(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        public bool UserHasRole(string roleName)
        {
            if (User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return false;
            }

            var claimsIdentity = User.Identity as ClaimsIdentity;

            return claimsIdentity.FindAll(ClaimTypes.Role).Any(role => role.Value == roleName);
        }

        public virtual PageResult PageWithError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Response.StatusCode = StatusCodes.Status400BadRequest;

            _notyfService.Error(errorMessage);

            return base.Page();
        }

        public virtual PageResult PageWithSuccess(string successMessage)
        {
            _notyfService.Success(successMessage);

            return this.Page();
        }

        public override PageResult Page()
        {
            ErrorMessage = null;

            return base.Page();
        }
    }
}
