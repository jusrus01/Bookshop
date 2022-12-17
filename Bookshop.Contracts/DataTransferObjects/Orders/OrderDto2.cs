using Bookshop.Contracts.Enums;

namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class OrderDto2
    {
        public double Sum { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string ClientComment { get; set; }

        public OrderMethod OrderMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public DateTime PaymentDate { get; set; }

        public List<OrderStateDto> States { get; set; }

        public List<OrderBookDto2> Books { get; set; }
    }
}
