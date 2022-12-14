using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Client
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client, BookshopRoles.Administrator, BookshopRoles.Manager)]
    public class ProfileModel : BookshopPageModel
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [BindProperty]
        public ClientViewModel ClientOutput { get; set; } = new();

        public ProfileModel(
            INotyfService notyfService, 
            IClientService clientService,
            IMapper mapper)
            :
            base(notyfService)
        {
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            try
            {
                var clientDto = await _clientService.GetAsync(userId);
                ClientOutput = _mapper.Map<ClientViewModel>(clientDto);
                return Page();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
