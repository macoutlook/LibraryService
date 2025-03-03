using Core.Application.Contract;
using Core.Domain;
using Core.Persistence.Contract;

namespace Core.Application;

public class BookApplicationService(IBookRepository repository, BookStateMachineValidator bookStateMachineValidator)
    : IBookApplicationService
{
    public async Task<ulong> AddBookAsync(Book book)
    {
        return await repository.AddAsync(book).ConfigureAwait(false);
    }

    public async Task UpdateBookAsync(Book book)
    {
        await bookStateMachineValidator.ValidateAsync(book.Id, book.BookStatus);
        await repository.UpdateAsync(book);
    }

    public async Task UpdateStatusAsync(ulong id, BookStatus bookStatus)
    {
        await bookStateMachineValidator.ValidateAsync(id, bookStatus);
        await repository.UpdateStatusAsync(id, bookStatus.ToString());
    }

    public async Task DeleteBookAsync(ulong id)
    {
        await repository.DeleteAsync(id);
    }

    public async Task<Book?> GetBookAsync(ulong id)
    {
        return await repository.GetAsync(id);
    }

    public async Task<IReadOnlyList<Book>> GetBooksAsync(string? title, string? author, string? isbn, BookStatus? status, int skip = 0, int take = 5)
    {
        return await repository.GetManyAsync(title, author, isbn, status.ToString(), skip, take);
    }
}