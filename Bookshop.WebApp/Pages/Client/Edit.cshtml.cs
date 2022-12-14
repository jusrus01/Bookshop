using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Clients;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Client
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client, BookshopRoles.Administrator)]
    public class EditModel : BookshopPageModel
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [BindProperty]
        public EditClientViewModel Input { get; set; } = new();

        public EditModel(
            IClientService clientService,
            IMapper mapper,
            INotyfService notyfService)
            :
            base(notyfService) 
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(Input.Id);
            }

            await _clientService.UpdateAsync(_mapper.Map<EditClientDto>(Input));
            return RedirectToPage("Profile", new { userId = Input.Id });
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            try
            {
                var client = await _clientService.GetAsync(userId);
                var roles = await _clientService.GetRolesAsync();
                
                Input = _mapper.Map<EditClientViewModel>(
                    client,
                    opt => opt.AfterMap((_, edit) => 
                    {
                        edit.AvailableRoles = roles;
                        edit.Role = client.Roles.First();
                    }));
                
                return Page();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
