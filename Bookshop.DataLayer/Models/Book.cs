using System.ComponentModel.DataAnnotations;

namespace Bookshop.DataLayer.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(10)]
        public string ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime AddedDate { get; set; }
        public double Discount { get; set; }
        public string Genre { get; set; }

    }
}
