namespace Bookshop.Contracts.DataTransferObjects.Clients
{
    public class ClientDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastLogin { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
