using Core.Application.Contract;
using Core.Domain;
using Core.Persistence.Contract;

namespace Core.Application;

public class BookApplicationService(IBookRepository repository) : IBookApplicationService
{
    public async Task<ulong> AddBookAsync(Book book)
    {
        return await repository.AddAsync(book).ConfigureAwait(false);
    }

    public async Task UpdateBookAsync(Book book)
    {
        await repository.UpdateAsync(book);
    }

    public async Task UpdateStatusAsync(ulong id, Status status)
    {
        await repository.UpdateStatusAsync(id, status.ToString());
    }

    // public async Task<Book?> GetArticleAsync(int id)
    // {
    //     return await _repository.GetArticleAsync(id).ConfigureAwait(false);
    // }
    //
    // public async Task<IEnumerable<Book>> GetArticlesAsync(string? category, int skip = 0, int take = 5)
    // {
    //     return await _repository.GetArticlesAsync(category, skip, take).ConfigureAwait(false);
    // }
}