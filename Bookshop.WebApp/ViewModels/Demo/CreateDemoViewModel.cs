using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Demo
{
    // Add validation stuff here
    public class CreateDemoViewModel
    {
        [Required] // <-
        [MaxLength(5)]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }
    }
}
