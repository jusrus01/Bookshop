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
        private const int PageSize = 4;

        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ListModel(IClientService clientService, IMapper mapper)
            :
            base(null)
        {
            _mapper = mapper;
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var clients = await _clientService.GetClientsPagedAsync(pageNumber, PageSize);
            SetPageItems(_mapper.Map<Paged<PartialClientViewModel>>(clients), pageNumber);
            return Page();
        }
    }
}
