using Bookshop.Contracts.Enums;
using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class OrderState : ICreationTimestamp
    {
        public int Id { get; set; }

        public DateTime Created { get; init; }

        public OrderStatus Status { get; set; }

        public string Comment { get; set; }
    }
}
