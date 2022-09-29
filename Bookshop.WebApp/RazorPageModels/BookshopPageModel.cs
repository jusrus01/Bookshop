using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Bookshop.WebApp.RazorPageModels
{
    [PageModel]
    public abstract class BookshopPageModel : PageModel
    {
        public virtual string? ErrorMessage { get; set; }

        public virtual bool HasError 
        { 
            get => ErrorMessage != null;
        }

        public virtual PageResult PageWithError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Response.StatusCode = StatusCodes.Status400BadRequest;

            return base.Page();
        }

        public override PageResult Page()
        {
            ErrorMessage = null;

            return base.Page();
        }
    }
}
