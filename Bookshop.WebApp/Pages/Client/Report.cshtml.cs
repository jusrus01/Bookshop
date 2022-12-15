using AspNetCoreHero.ToastNotification.Abstractions;
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
    public class ReportModel : BookshopPageModel
    {
        [BindProperty]
        public ClientReportViewModel Output { get; set; } = new ();

        private readonly IClientService _clientService;

        public ReportModel(IClientService clientService, INotyfService notyfService) : base(notyfService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var orders = await _clientService.GetOrderHistoryAsync(userId);
            Set(orders);
            return Page();
        }

        //public async Task<IActionResult> OnGetDownloadAsync()
        //{
        //    return File(await _clientService.GetOrderHistoryPdfAsync("asdas"), "application/pdf", "somename.pdf");
        //}

        private void Set(IEnumerable<ClientReportOrderDto> orders)
        {
            Output = new ClientReportViewModel
            {
                Orders = orders.ToList()
            };
        }
    }
}
