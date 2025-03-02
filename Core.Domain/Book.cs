using System.Runtime.Serialization;

namespace Core.Domain;

public class Book
{
    public ulong Id { get; set; }

    public required string Title { get; init; }

    public required string Author { get; init; }

    public required string Isbn { get; init; }
    public Status Status { get; init; }
}

public enum Status
{
    [EnumMember(Value = "OnShelf")] OnShelf,
    [EnumMember(Value = "CheckedOut")] CheckedOut,
    [EnumMember(Value = "Returned")] Returned,
    [EnumMember(Value = "BrokenDown")] BrokenDown
}