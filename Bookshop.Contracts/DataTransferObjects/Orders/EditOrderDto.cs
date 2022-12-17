using Bookshop.Contracts.Enums;

namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class EditOrderDto
    {
        public int Id { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string CourierComment { get; set; }

        public OrderMethod DeliveryMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }
    }
}
