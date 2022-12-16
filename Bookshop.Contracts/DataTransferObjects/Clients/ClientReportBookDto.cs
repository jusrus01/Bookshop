namespace Bookshop.Contracts.DataTransferObjects.Clients
{
    public class ClientReportBookDto
    {
        public string Author { get; set; }

        public string Title { get; set; }

        public int Pages { get; set; }

        public double Price { get; set; }
    }
}
