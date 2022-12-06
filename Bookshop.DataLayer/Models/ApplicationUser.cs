using Bookshop.DataLayer.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Bookshop.DataLayer.Models
{
    public class ApplicationUser : IdentityUser, ICreationTimestamp
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime Created { get; init; }

        public DateTime LastLogin { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
