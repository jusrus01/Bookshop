namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class CreateOrderBookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
