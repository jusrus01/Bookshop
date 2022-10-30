namespace Bookshop.Contracts.Generics
{
    public class Paged<T> where T : class
    {
        public int CurrentPage { get; set; }

        public IEnumerable<T> Items { get; set; }

        public bool NextPageIsEmpty { get; set; }

        public bool PreviousPageIsEmpty { get; set; }

        public int Count { get; set; }
    }
}
