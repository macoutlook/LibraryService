using Core.Application;
using Core.Application.Contract;
using Core.Persistence.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.ArticleRepository;
using Book = Persistence.ArticleRepository.DbModels.Book;

namespace Bootstrapper;

public static class Bootstrapper
{
    public static void RegisterApplication(this IServiceCollection services)
    {
        services.AddScoped<BookStateMachine>();
        services.AddScoped<IBookApplicationService, BookApplicationService>();
    }

    public static void RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookStatusRepository, BookRepository>();
        services.AddDbContext<BookContext>(options =>
            options.UseSqlServer(configuration.GetSection("ConnectionString").Value));
    }

    public static async Task InitializeDb(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseSqlServer(configuration.GetSection("ConnectionString").Value).Options;
        await using var context = new BookContext(options);

        if (await context.Database.EnsureCreatedAsync())
            await RunDataFeed(configuration);
    }

    private static async Task RunDataFeed(IConfiguration configuration)
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseSqlServer(configuration.GetSection("ConnectionString").Value).Options;
        var context = new BookContext(options);

        context.Books.AddRange(
            new Book
            {
                Title = "To Kill a Mockingbird", Author = "Harper Lee", Isbn = "978-83-01-00001-1", Status = "OnShelf"
            },
            new Book
            {
                Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Isbn = "978-83-01-00002-3",
                Status = "CheckedOut"
            },
            new Book
            {
                Title = "Pride and Prejudice", Author = "Jane Austen", Isbn = "978-83-01-00002-4", Status = "Returned"
            },
            new Book
            {
                Title = "The Catcher in the Rye", Author = "J.D. Salinger", Isbn = "978-83-01-00002-5",
                Status = "OnShelf"
            },
            new Book
            {
                Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Isbn = "978-83-01-00002-6",
                Status = "CheckedOut"
            },
            new Book
            {
                Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Isbn = "978-83-01-00002-6",
                Status = "Returned"
            },
            new Book
            {
                Title = "The Lion, the Witch and the Wardrobe", Author = "C.S. Lewis", Isbn = "978-84-01-00002-3",
                Status = "OnShelf"
            },
            new Book
            {
                Title = "The Handmaid's Tale", Author = "Margaret Atwood", Isbn = "978-83-01-00022-3",
                Status = "CheckedOut"
            },
            new Book
            {
                Title = "The Nightingale", Author = "Kristin Hannah", Isbn = "978-85-01-00002-3", Status = "Returned"
            },
            new Book
            {
                Title = "The Hate U Give", Author = "Angie Thomas", Isbn = "978-43-01-00002-3", Status = "OnShelf"
            }
        );
        await context.SaveChangesAsync();
        await context.DisposeAsync();
    }
}