using Core.Domain;

namespace Core.Application.Contract;

public interface IBookApplicationService
{
    public Task<ulong> AddBookAsync(Book book, CancellationToken cancellationToken = default);
    public Task UpdateBookAsync(Book book, CancellationToken cancellationToken = default);
    public Task UpdateStatusAsync(ulong id, BookStatus bookStatus, CancellationToken cancellationToken = default);
    public Task DeleteBookAsync(ulong id, CancellationToken cancellationToken = default);
    public Task<Book?> GetBookAsync(ulong id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Book>> GetBooksAsync(string? title, string? author, string? isbn, BookStatus? status,
        int skip = 0, int take = 5, CancellationToken cancellationToken = default);
}