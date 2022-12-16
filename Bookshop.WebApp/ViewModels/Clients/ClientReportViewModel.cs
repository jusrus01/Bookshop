using Bookshop.Contracts.DataTransferObjects.Clients;

namespace Bookshop.WebApp.ViewModels.Clients
{
    public class ClientReportViewModel
    {
        public string Id { get; set; }

        public List<ClientReportOrderDto> Orders { get; set; }
    }
}
