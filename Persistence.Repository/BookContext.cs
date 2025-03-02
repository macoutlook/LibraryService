using Microsoft.EntityFrameworkCore;
using Persistence.ArticleRepository.DbModels;

namespace Persistence.ArticleRepository;

/// <summary>
///     Database context from which db migration was generated.
///     To create database from migration, run bekow command in Package Manager Console:
///     > Update-Database
/// </summary>
public class BookContext(DbContextOptions<BookContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(b => b.Isbn)
            .HasDatabaseName("IX_Isbn")
            .IsUnique()
            .IsClustered(false);
    }
}