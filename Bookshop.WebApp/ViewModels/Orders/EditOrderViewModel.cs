using Bookshop.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.WebApp.ViewModels.Orders
{
    public class EditOrderViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string CourierComment { get; set; }

        [Required]
        public OrderMethod? DeliveryMethod { get; set; }

        [Required]
        public PaymentMethod? PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }
    }
}
