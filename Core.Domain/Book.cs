using System.Runtime.Serialization;

namespace Core.Domain;

public class Book
{
    public ulong Id { get; init; }

    public string Title { get; init; } = null!;

    public string Author { get; init; } = null!;

    public string Isbn { get; init; } = null!;
    public BookStatus BookStatus { get; init; }
}

public enum BookStatus
{
    [EnumMember(Value = "OnShelf")] OnShelf,
    [EnumMember(Value = "CheckedOut")] CheckedOut,
    [EnumMember(Value = "Returned")] Returned,
    [EnumMember(Value = "BrokenDown")] BrokenDown
}