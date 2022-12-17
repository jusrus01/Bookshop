namespace Bookshop.Contracts.Enums
{
    public enum OrderMethod
    {
        PostMachine,
        Courier
    }

    public static class OrderMethodExtensions
    {
        public static string GetString(this OrderMethod method) =>
            method switch
            {
                OrderMethod.PostMachine => "Self-service terminal",
                OrderMethod.Courier => "Courier",
                _ => ""
            };
    }
}
