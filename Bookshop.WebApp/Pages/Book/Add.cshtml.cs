using Bookshop.Contracts.Constants;
using Bookshop.WebApp.Attributes;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator)]
    public class AddModel
    {
    }
}