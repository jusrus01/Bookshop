using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.DataLayer.Models
{
    public class Demo
    {
        //[Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Value { get; set; }

        // Adding reference to IdentityUser (which is either Administrator, Client or Manager...)
        public IdentityUser User { get; set; }

        public string UserId { get; set; }
    }
}
