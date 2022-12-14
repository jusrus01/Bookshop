using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Books
{
    public class CommentViewModel
    {
        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "ClientName")]
        public string ClientName { get; set; }

        [Display(Name = "BookId")]
        public int BookId { get; set; }
    }
}
