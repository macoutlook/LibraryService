using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Persistence.Contract;

namespace Persistence.ArticleRepository;

public class BookRepository : IBookRepository
{
    private readonly BookContext _context;
    private readonly IMapper _mapper;

    public BookRepository(BookContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ulong> AddAsync(Book book)
    {
        var bookDb = new DbModels.Book
        {
            Title = book.Title,
            Author = book.Author,
            Isbn = book.Isbn,
            Status = book.Status.ToString()
        };

        await _context.Books.AddAsync(bookDb);
        await _context.SaveChangesAsync();

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
            Status = book.Status.ToString()
        };

        _context.Books.Update(bookDb);
        await _context.SaveChangesAsync();
    }

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