using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Contracts.DataTransferObjects.Orders
{
    public class PartialOrderDto
    {
        public int Id { get; set; }
        public string ClientName { get; set; }

        public DateTime Created { get; init; }

        public double Sum { get; set; }

        public string PostalCode { get; set; }
    }
}
