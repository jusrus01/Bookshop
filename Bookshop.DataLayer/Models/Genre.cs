using System.ComponentModel.DataAnnotations;

namespace Bookshop.DataLayer.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
