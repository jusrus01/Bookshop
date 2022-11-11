using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;

namespace Bookshop.WebApp.Pages.Client
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client)]
    public class EditModel : BookshopPageModel
    {
        public EditModel() : base(null) 
        {
        }

        public void OnGet()
        {
        }
    }
}
