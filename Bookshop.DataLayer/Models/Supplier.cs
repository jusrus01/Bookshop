using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class Supplier : IKeyable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }

        public IList<Storage> Storages { get; set; }
    }
}
