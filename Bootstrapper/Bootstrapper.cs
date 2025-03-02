using Core.Application;
using Core.Application.Contract;
using Core.Domain;
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
        services.AddScoped<IBookApplicationService, BookApplicationService>();
    }

    public static void RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddDbContext<BookContext>(options =>
            options.UseSqlServer(configuration.GetSection("ConnectionString").Value));
    }

    public static async Task InitializeDb(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseSqlServer(configuration.GetSection("ConnectionString").Value).Options;
        await using var context = new BookContext(options);

        await context.Database.EnsureCreatedAsync();
    }

    public static void RunDataFeed(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new DbContextOptionsBuilder<BookContext>()
            .UseSqlServer(configuration.GetSection("ConnectionString").Value).Options;
        var context = new BookContext(options);

        context.Books.AddRange(
            new Book
            {
                Title = "To Kill a Mockingbird", Author = "Harper Lee", Isbn = "978-0446310789", Status = "OnShelf"
            },
            new Book
            {
                Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Isbn = "978-0743273565",
                Status = "CheckedOut"
            },
            new Book
            {
                Title = "Pride and Prejudice", Author = "Jane Austen", Isbn = "978-1853260509", Status = "Returned"
            },
            new Book
            {
                Title = "The Catcher in the Rye", Author = "J.D. Salinger", Isbn = "978-0316769174",
                Status = "OnShelf"
            },
            new Book
            {
                Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Isbn = "978-1400052929",
                Status = "CheckedOut"
            },
            new Book
            {
                Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Isbn = "978-0547928217",
                Status = "Returned"
            },
            new Book
            {
                Title = "The Lion, the Witch and the Wardrobe", Author = "C.S. Lewis", Isbn = "978-0060234816",
                Status = "OnShelf"
            },
            new Book
            {
                Title = "The Handmaid's Tale", Author = "Margaret Atwood", Isbn = "978-0385490818",
                Status = "CheckedOut"
            },
            new Book
            {
                Title = "The Nightingale", Author = "Kristin Hannah", Isbn = "978-0312577223", Status = "Returned"
            },
            new Book
            {
                Title = "The Hate U Give", Author = "Angie Thomas", Isbn = "978-0062498533", Status = "OnShelf"
            }
        );
        context.SaveChanges();
        context.Dispose();
    }
}