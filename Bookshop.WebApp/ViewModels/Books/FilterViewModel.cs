using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Books
{
    public class FilterViewModel
    {
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "MinPrice")]
        public double MinPrice { get; set; }

        [Display(Name = "MaxPrice")]
        public double MaxPrice { get; set; }

        [Display(Name = "MinDiscount")]
        public int MinDiscount { get; set; }

        [Display(Name = "MaxDiscount")]
        public int MaxDiscount { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}
