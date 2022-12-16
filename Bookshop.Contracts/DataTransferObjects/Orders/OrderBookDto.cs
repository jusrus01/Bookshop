using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class OrderBookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
        public double Discount { get; set; }
        public int OrderId { get; set; }
        public int GenreId { get; set; }
    }
}
