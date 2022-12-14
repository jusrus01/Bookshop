using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Client
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client, BookshopRoles.Administrator)]
    public class ListModel : SinglePaginationBookshopPagedModel<PartialClientViewModel>
    {
        private const int PageSize = 8;

        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ListModel(
            IClientService clientService,
            IMapper mapper,
            INotyfService notyfService)
            :
            base(notyfService)
        {
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            try
            {
                await _clientService.DeleteAsync(id);
                return await OnGetAsync();
            }
            catch (Exception ex)
            {
                return await PageWithErrorAsync(ex.Message, () => OnGetAsync());
            }
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var clients = await _clientService.GetPagedAsync(pageNumber, PageSize);
            SetPageItems(_mapper.Map<Paged<PartialClientViewModel>>(clients), pageNumber);
            return Page();
        }
    }
}
