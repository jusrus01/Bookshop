using Bookshop.DataLayer.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Bookshop.DataLayer.Models
{
    public class Book : ICreationTimestamp, IKeyable
    {
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public int Year { get; set; }

        public int Pages { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        public double Discount { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public DateTime Created { get; init; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
