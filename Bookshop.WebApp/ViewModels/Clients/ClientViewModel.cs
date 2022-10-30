using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Clients
{
    public class ClientViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Created")]
        public DateTime Created { get; set; }

        [Display(Name = "Last login")]
        public DateTime LastLogin { get; set; }

        [Display(Name = "Roles")]
        public IEnumerable<string> Roles { get; set; }
    }
}
