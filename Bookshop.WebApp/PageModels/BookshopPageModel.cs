using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
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
            SetError(errorMessage);
            return base.Page();
        }

        public virtual async Task<PageResult> PageWithErrorAsync(string errorMessage, Func<Task<IActionResult>> errorPage)
        {
            SetError(errorMessage);
            return (PageResult)await errorPage();
        }

        public virtual PageResult PageWithSuccess(string successMessage)
        {
            _notyfService.Success(successMessage);
            return this.Page();
        }

        public override PageResult Page()
        {
            ErrorMessage = null;
            return base.Page();
        }

        private void SetError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Response.StatusCode = StatusCodes.Status400BadRequest;
            _notyfService.Error(errorMessage);
        }
    }
}
