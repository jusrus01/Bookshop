using Bookshop.DataLayer.Contracts;

namespace Bookshop.DataLayer.Models
{
    public class City : IKeyable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
