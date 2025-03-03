using Core.Application.Contract;
using Core.Application.DomainServices;
using Core.Domain;
using Core.Persistence.Contract;

namespace Core.Application;

public class BookApplicationService(
    IBookRepository repository,
    IBookStateMachineValidator bookStateMachineValidator)
    : IBookApplicationService
{
    public async Task<ulong> AddBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        return await repository.AddAsync(book, cancellationToken);
    }

    public async Task UpdateBookAsync(Book book, CancellationToken cancellationToken = default)
    {
        await bookStateMachineValidator.ValidateAsync(book.Id, book.BookStatus);
        await repository.UpdateAsync(book, cancellationToken);
    }

    public async Task UpdateStatusAsync(ulong id, BookStatus bookStatus, CancellationToken cancellationToken = default)
    {
        await bookStateMachineValidator.ValidateAsync(id, bookStatus);
        await repository.UpdateStatusAsync(id, bookStatus.ToString(), cancellationToken);
    }

    public async Task DeleteBookAsync(ulong id, CancellationToken cancellationToken = default)
    {
        await repository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Book?> GetBookAsync(ulong id, CancellationToken cancellationToken = default)
    {
        return await repository.GetAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<Book>> GetBooksAsync(string? title, string? author, string? isbn,
        BookStatus? status, int skip = 0, int take = 5, CancellationToken cancellationToken = default)
    {
        return await repository.GetManyAsync(title, author, isbn, status.ToString(), skip, take, cancellationToken);
    }
}