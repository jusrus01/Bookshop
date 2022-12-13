using Bookshop.Contracts.Enums;
using Bookshop.DataLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public double Sum { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string ClientComment { get; set; }

        public OrderMethod OrderMethod { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public DateTime PaymentDate { get; set; }

        public OrderStatus Status { get; set; }
        public string UserId { get; set; }
        public string BookId { get; set; }


    }
}
