using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Persistence.Contract;
using Microsoft.EntityFrameworkCore;

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

    public async Task<int> SaveBookAsync(Book book)
    {
        // var newArticle = new DbModels.Article
        // {
        //     Category = articleDomain.Category,
        //     Content = articleDomain.Content,
        //     Title = articleDomain.Title
        // };
        //
        // await _context.Articles.AddAsync(newArticle).ConfigureAwait(false);
        // await _context.SaveChangesAsync().ConfigureAwait(false);
        //
        // return newArticle.Id != 0
        //     ? newArticle.Id
        //     : throw new NoRecordCreatedException("Cannot create new article with sent request");
        return await Task.FromResult(1);
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