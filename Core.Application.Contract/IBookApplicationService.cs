using Core.Domain;

namespace Core.Application.Contract;

public interface IBookApplicationService
{
    // public Task<Book?> GetArticleAsync(int id);
    //
    // public Task<IEnumerable<Book>> GetArticlesAsync(string? category, int skip = 0, int take = 5);

    public Task<ulong> AddBookAsync(Book book);
    public Task UpdateBookAsync(Book book);
}