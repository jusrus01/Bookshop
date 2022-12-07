namespace Bookshop.Contracts.DataTransferObjects.Books
{
    public class PartialBookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Genre { get; set; }
        public double PriceWithDiscount { get; set; }
    }
}
