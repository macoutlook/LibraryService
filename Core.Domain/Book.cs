using System.Runtime.Serialization;

namespace Core.Domain;

public class Book
{
    public ulong Id { get; init; }

    public string Title { get; init; } = null!;

    public string Author { get; init; } = null!;

    public string Isbn { get; init; } = null!;
    public Status Status { get; init; }
}

public enum Status
{
    [EnumMember(Value = "OnShelf")] OnShelf,
    [EnumMember(Value = "CheckedOut")] CheckedOut,
    [EnumMember(Value = "Returned")] Returned,
    [EnumMember(Value = "BrokenDown")] BrokenDown
}