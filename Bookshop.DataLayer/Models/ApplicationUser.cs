using Microsoft.AspNetCore.Identity;

namespace Bookshop.DataLayer.Models
{
    // Overriding base values for clarity
    public class ApplicationUser : IdentityUser
    {
        public override string UserName { get; set; }

        public string LastName { get; set; }

        public override string PhoneNumber { get; set; }

        public string Address { get; set; }

        public override bool EmailConfirmed { get; set; }

        public override string Email { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastLogin { get; set; }
    }
}
