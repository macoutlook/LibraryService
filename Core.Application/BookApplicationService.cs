using Core.Application.Contract;
using Core.Domain;
using Core.Persistence.Contract;

namespace Core.Application;

public class BookApplicationService(IBookRepository repository, BookStateMachine bookStateMachine)
    : IBookApplicationService
{
    public async Task<ulong> AddBookAsync(Book book)
    {
        return await repository.AddAsync(book).ConfigureAwait(false);
    }

    public async Task UpdateBookAsync(Book book)
    {
        await bookStateMachine.DoTransitionAsync(book.Id, book.BookStatus);
        await repository.UpdateAsync(book);
    }

    public async Task UpdateStatusAsync(ulong id, BookStatus bookStatus)
    {
        await bookStateMachine.DoTransitionAsync(id, bookStatus);
        await repository.UpdateStatusAsync(id, bookStatus.ToString());
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