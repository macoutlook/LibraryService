using System.ComponentModel.DataAnnotations;

namespace Persistence.ArticleRepository.DbModels;

public class Book
{
    public ulong Id { get; init; }

    [MaxLength(320, ErrorMessage = "Title must be 320 characters or less")]
    [MinLength(1)]
    public string Title { get; init; } = null!;

    [MaxLength(320, ErrorMessage = "Author must be 320 characters or less")]
    [MinLength(3)]
    public string Author { get; init; } = null!;

    [MaxLength(17, ErrorMessage = "Isbn must be 17 characters")]
    [MinLength(17)]
    public string Isbn { get; init; } = null!;

    [MaxLength(20)] [MinLength(5)] public required string Status { get; init; }
}