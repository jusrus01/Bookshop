using AutoMapper;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Client
{
    public class ListModel : SinglePaginationBookshopPagedModel<PartialClientViewModel>
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        private const int PageSize = 4;

        public ListModel(IClientService clientService, IMapper mapper)
            :
            base(null)
        {
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            CurrentPage = pageNumber;

            var clients = await _clientService.GetClientsPagedAsync(CurrentPage, PageSize);

            Paged = _mapper.Map<Paged<PartialClientViewModel>>(clients);

            return Page();
        }
    }
}
