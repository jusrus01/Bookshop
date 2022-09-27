using Microsoft.AspNetCore.Identity;

namespace Bookshop.DataLayer.Models
{
    public class Demo
    {
        public string Name { get; set; }

        public int Value { get; set; }

        // Adding reference to IdentityUser (which is either Administrator, Client or Manager...)
        public IdentityUser User { get; set; }

        public string UserId { get; set; }
    }
}
