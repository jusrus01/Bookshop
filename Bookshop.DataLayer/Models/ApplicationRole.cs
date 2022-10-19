using Microsoft.AspNetCore.Identity;

namespace Bookshop.DataLayer.Models
{
    // Overriding base values for clarity
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name, DateTime created)
        {
            Name = name;
            Created = created;
        }

        public override string Name { get; set; }

        public DateTime Created { get; set; }
    }
}
