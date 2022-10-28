using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace Bookshop.WebApp.PageModels
{
    [PageModel]
    public abstract class BookshopPageModel : PageModel
    {
        protected readonly INotyfService _notyfService;

        public virtual string ErrorMessage { get; set; }

        public virtual bool HasError
        {
            get => ErrorMessage != null;
        }

        public BookshopPageModel(INotyfService notyfService)
        {
            _notyfService = notyfService;
        }

        public virtual PageResult PageWithError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Response.StatusCode = StatusCodes.Status400BadRequest;

            _notyfService.Error(errorMessage);

            return base.Page();
        }

        public override PageResult Page()
        {
            ErrorMessage = null;

            return base.Page();
        }
    }
}
