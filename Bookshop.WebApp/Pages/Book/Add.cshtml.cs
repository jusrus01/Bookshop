using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator)]
    public class AddModel : BookshopPageModel
    {
        public AddModel(INotyfService notyfService) : base(null)
        {
        }
    }
}