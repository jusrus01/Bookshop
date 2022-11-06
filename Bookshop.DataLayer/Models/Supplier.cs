using System.ComponentModel.DataAnnotations;

namespace Bookshop.DataLayer.Models
{
    public class Supplier
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
    }
}
