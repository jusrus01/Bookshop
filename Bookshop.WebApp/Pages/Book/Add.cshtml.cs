using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;

namespace Bookshop.WebApp.Pages.Book
{
    [RolesAuthorize(BookshopRoles.Administrator)]
    public class AddModel
    {
    }
}