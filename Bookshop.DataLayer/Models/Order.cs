using Bookshop.Contracts.Enums;
using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class Order : ICreationTimestamp
    {
        public int Id { get; set; }

        public DateTime Created { get; init; }

        public double Sum { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string ClientComment { get; set; }

        public Bookshop.Contracts.Enums.OrderMethod OrderMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public DateTime PaymentDate { get; set; }

        public string CourierComment { get; set; }

        public ApplicationUser Client { get; set; }

        public int ClientId { get; set; }

        public OrderState Status { get; set; }

        public int StatusId { get; set; }
    }
}
