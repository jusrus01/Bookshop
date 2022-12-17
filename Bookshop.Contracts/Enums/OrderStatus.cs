namespace Bookshop.Contracts.Enums
{
    public enum OrderStatus
    {
        Completed,
        Ongoing,
        NotPayed
    }

    public static class OrderStatusExtensions
    {
        public static string GetString(this OrderStatus status) =>
            status switch
            {
                OrderStatus.Completed => "Completed",
                OrderStatus.Ongoing => "Processing",
                OrderStatus.NotPayed => "Not payed",
                _ => ""
            };
    }
}
