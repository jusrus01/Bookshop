namespace Bookshop.Contracts.Enums
{
    public enum PaymentMethod
    {
        BankTransfer,
        DuringDelivery
    }

    public static class PaymentMethodExtensions
    {
        public static string GetString(this PaymentMethod method) =>
            method switch
            {
                PaymentMethod.BankTransfer => "Bank transfer",
                PaymentMethod.DuringDelivery => "In cash",
                _ => ""
            };
    }
}
