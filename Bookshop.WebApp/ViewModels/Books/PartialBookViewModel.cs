namespace Bookshop.WebApp.ViewModels.Clients
{
    public class PartialBookViewModel
    {
        public int Id { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }
    }
}
