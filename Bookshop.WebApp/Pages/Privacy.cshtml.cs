using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages
{
    [RolesAuthorize(BookshopRoles.Administrator)]
    public class PrivacyModel : PageModel
    {
        public PrivacyModel()
        {
        }

        public void OnGet()
        {
        }
    }
}