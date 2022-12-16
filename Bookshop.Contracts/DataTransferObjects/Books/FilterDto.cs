namespace Bookshop.Contracts.DataTransferObjects.Books
{
    public class FilterDto
    {
        public string Author { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }

        public double MinDicount { get; set; }

        public double MaxDicount { get; set; }

        public string Title { get; set; }
    }
}
