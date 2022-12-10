using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Supplier")]
        public string Supplier { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Pages")]
        public int Pages { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Added date")]
        public DateTime AddedDate { get; set; }

        [Display(Name = "Discount")]
        public double Discount { get; set; }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

    }
}
