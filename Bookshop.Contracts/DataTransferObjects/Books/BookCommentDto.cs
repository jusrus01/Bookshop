namespace Bookshop.Contracts.DataTransferObjects.Books
{
    public class BookCommentDto
    {
        public int Score { get; set; }

        public string Comment { get; set; }

        public string UserId { get; set; }

        public int BookId { get; set; }

    }
}
