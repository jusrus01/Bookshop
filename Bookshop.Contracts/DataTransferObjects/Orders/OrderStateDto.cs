using Bookshop.Contracts.Enums;

namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class OrderStateDto
    {
        public OrderStatus Status { get; set; }

        public DateTime Created { get; set; }

        public string Comment { get; set; }
    }
}
