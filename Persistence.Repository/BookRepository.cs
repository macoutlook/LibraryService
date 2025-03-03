using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Persistence.Contract;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace Persistence.BookRepository;

public class BookRepository(BookContext context, IMapper mapper) : IBookRepository
{
    # region Command

    public async Task<ulong> AddAsync(Book book)
    {
        var bookDb = new DbModels.Book
        {
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Status = book.BookStatus.ToString()
        };

        await context.Books.AddAsync(bookDb);
        await context.SaveChangesAsync();

        return bookDb.Id != 0
            ? bookDb.Id
            : throw new NoRecordCreatedException("Cannot create new article with sent request");
    }

    public async Task UpdateAsync(Book book)
    {
        var bookDb = new DbModels.Book
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Status = book.BookStatus.ToString()
        };

        context.Books.Update(bookDb);
        await context.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(ulong id, string status)
    {
        var book = new DbModels.Book { Id = id, Status = status };

        context.Books.Attach(book);
        context.Entry(book).Property(b => b.Status).IsModified = true;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id.Equals(id));
        if (book is null)
            throw new InvalidOperationException("Book with given id does not exist.");
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }

    # endregion

    # region Query

    public async Task<Book?> GetAsync(ulong id)
    {
        return mapper.Map<Book>(await context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id));
    }

    public async Task<IReadOnlyList<Book>> GetManyAsync(string? title, string? author, string? isbn, string? status,
        int skip = 0, int take = 5)
    {
        var predicate = PredicateBuilder.New<DbModels.Book>();
        if (!string.IsNullOrWhiteSpace(title))
            predicate.And(b => b.Title.Equals(title));
        if (!string.IsNullOrWhiteSpace(author))
            predicate.And(b => b.Author.Equals(author));
        if (!string.IsNullOrWhiteSpace(isbn))
            predicate.And(b => b.Isbn.Equals(isbn));
        if (!string.IsNullOrWhiteSpace(status))
            predicate.And(b => b.Status.Equals(status));

        var results = await context.Books.Where(predicate)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return mapper.Map<List<Book>>(results);
    }

    public async Task<string> GetStatusAsync(ulong id)
    {
        var book = await context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if (book is null)
            throw new InvalidOperationException("Book with given id does not exist.");

        return book.Status;
    }

    # endregion
}