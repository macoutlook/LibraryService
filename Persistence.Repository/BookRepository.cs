using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Persistence.Contract;
using Microsoft.EntityFrameworkCore;

namespace Persistence.ArticleRepository;

public class BookRepository(BookContext context, IMapper mapper) : IBookRepository
{
    private readonly IMapper _mapper = mapper;

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
        if(book is null)
            throw new InvalidOperationException("Book with given id does not exist.");
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }

    # endregion

    # region Query
    
    public async Task<Book?> GetAsync(ulong id)
    {
        return _mapper.Map<Book>(await context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id));
    }
    
    public async Task<string> GetStatusAsync(ulong id)
    {
        var book = await context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        if(book is null)
            throw new InvalidOperationException("Book with given id does not exist."); 
        
        return book.Status;
    }
    
    # endregion
    



    // public async Task<Article?> GetArticleAsync(int id)
    // {
    //     var article = await _context.Articles.FirstOrDefaultAsync(f => f.Id.Equals(id)).ConfigureAwait(false);
    //     return article == null ? null : _mapper.Map<Article>(article);
    // }
    //
    // public async Task<IEnumerable<Article>> GetArticlesAsync(string? category, int skip = 0, int take = 5)
    // {
    //     var results =
    //         await _context.Articles.Where(a => !string.IsNullOrEmpty(category) ? a.Category.Equals(category) : true)
    //             .Skip(skip)
    //             .Take(take)
    //             .ToListAsync().ConfigureAwait(false);
    //
    //     return _mapper.Map<List<Article>>(results);
    // }
}