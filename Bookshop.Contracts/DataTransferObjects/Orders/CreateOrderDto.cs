using Bookshop.Contracts.Enums;

namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class CreateOrderDto
    {
        public string ClientCommentForCourier { get; set; }

        public List<CreateOrderBookDto> SelectedBooks { get; set; }

        public OrderMethod DeliveryMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }
    }
}
