using Bookshop.DataLayer.Models;

namespace Bookshop.WebApp.ViewModels.Orders
{
    public class PartialOrderViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }

        public DateTime Created { get; init; }

        public double Sum { get; set; }

        public string PostalCode { get; set; }
    }
}
