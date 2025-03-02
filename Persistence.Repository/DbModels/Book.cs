using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.ArticleRepository.DbModels;

public class Book
{
    public ulong Id { get; init; }

    [MaxLength(320, ErrorMessage = "Title must be 320 characters or less")]
    [MinLength(1)]
    public required string Title { get; init; }

    [MaxLength(320, ErrorMessage = "Author must be 320 characters or less")]
    [MinLength(3)]
    public required string Author { get; init; }

    [MaxLength(17, ErrorMessage = "Isbn must be 17 characters")]
    [MinLength(17)]
    public required string Isbn { get; init; }
    
    [MaxLength(20)]
    [MinLength(5)]
    public required string Status { get; init; }
}