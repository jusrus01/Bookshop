using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client)]
    public class ListModel
    {
    }
}
