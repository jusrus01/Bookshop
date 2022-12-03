namespace Bookshop.DataLayer.Models
{
    public class Storage
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int EmployeeCount { get; set; }

        public int Capacity { get; set; }

        public int BookCount { get; set; }

        public IList<Supplier> Suppliers { get; set; }
    }
}
