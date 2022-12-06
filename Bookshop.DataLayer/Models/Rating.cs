using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class Rating : ICreationTimestamp, IKeyable
    {
        public int Id { get; set; }

        public DateTime Created { get; init; }
        
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int Score { get; set; }

        public string Comment { get; set; }
    }
}
