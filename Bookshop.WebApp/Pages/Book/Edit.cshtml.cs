using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;

namespace Bookshop.WebApp.Pages.Book
{
    [RolesAuthorize(BookshopRoles.Manager)]
    public class EditModel
    {
    }
}