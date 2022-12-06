using Bookshop.DataLayer.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.DataLayer.Models
{
    public class Genre : IKeyable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
