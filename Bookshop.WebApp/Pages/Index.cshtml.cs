using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }
    }
}