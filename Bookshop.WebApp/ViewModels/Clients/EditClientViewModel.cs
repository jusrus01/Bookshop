using Bookshop.Contracts.DataTransferObjects.Clients;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Clients
{
    public class EditClientViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Roles")]
        public IEnumerable<string> Roles { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string SelectedRole { get; set; }

        [Required]
        public string Role { get; set; }

        public IEnumerable<RoleDto> AvailableRoles { get; set; }
    }
}
