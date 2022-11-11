using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages.Suppliers
{
    public class ListModel : PageModel
    {
        [AuthorizeAnyOfTheRoles(BookshopRoles.Manager)]
        public void OnGet()
        {
        }
    }
}
