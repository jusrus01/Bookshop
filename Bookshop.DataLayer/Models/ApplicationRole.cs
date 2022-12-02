using Bookshop.DataLayer.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Bookshop.DataLayer.Models
{
    public class ApplicationRole : IdentityRole, ICreationTimestamp
    {
        public DateTime Created { get; init; }
    }
}
