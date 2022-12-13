using Bookshop.Contracts.Enums;
using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class Order : ICreationTimestamp, IKeyable
    {
        public int Id { get; set; }

        public DateTime Created { get; init; }

        public double Sum { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string ClientComment { get; set; }

        public OrderMethod OrderMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public DateTime PaymentDate { get; set; }

        public string CourierComment { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public OrderState Status { get; set; }

        public int StatusId { get; set; }
        public string BookId { get; set; }
    }
}
