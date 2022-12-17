using Bookshop.Contracts.Enums;
using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class OrderState : ICreationTimestamp, IKeyable
    {
        public int Id { get; set; }

        public DateTime Created { get; init; }

        public OrderStatus Status { get; set; }

        public Order Order { get; set; }

        public int OrderId { get; set; }

        public string Comment { get; set; }
    }
}
