using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Demo;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.ViewModels.Demo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages.Demo
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public CreateDemoViewModel CreateInput { get; set; }

        private readonly IMapper _mapper;
        private readonly ISomekindOfService _somekindOfService;

        public IndexModel(IMapper mapper, ISomekindOfService somekindOfService) // Using dependecy injection again
        {
            _mapper = mapper;
            _somekindOfService = somekindOfService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) // Validation attribute check failed
            {
                return Page();
            }

            var fromViewModelToDto = _mapper.Map<CreateDemoDto>(CreateInput);

            await _somekindOfService.CreateStuffAsync(fromViewModelToDto);

            return RedirectToPage("..//Index"); ;
        }
    }
}
