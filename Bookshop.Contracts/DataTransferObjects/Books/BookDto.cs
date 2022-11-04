namespace Bookshop.Contracts.DataTransferObjects.Books
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime AddedDate { get; set; }
        public double Discount { get; set; }
        public string Genre { get; set; }
    }
}
