namespace Bookshop.Contracts.DataTransferObjects.Demo
{
    public class CreateDemoDto // Usually has the same values as view model, although they can exist without it
    { // Also, do not include validation attributes here (or data annotations?)
        public string Name { get; set; }

        public int Value { get; set; }
    }
}
