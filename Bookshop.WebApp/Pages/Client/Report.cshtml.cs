using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;

namespace Bookshop.WebApp.Pages.Client
{
    [RolesAuthorize(BookshopRoles.Client)]
    public class ReportModel : BookshopPageModel
    {
        public ReportModel() : base(null)
        {
        }

        public void OnGet()
        {
        }
    }
}
