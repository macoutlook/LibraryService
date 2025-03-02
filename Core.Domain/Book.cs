namespace Core.Domain
{
    public class Book
    {
        public ulong Id { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required string Isbn { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        OnShelf,
        CheckedOut,
        Returned,
        BrokenDown
    }
}