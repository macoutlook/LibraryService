using Core.Domain;

namespace Core.Persistence.Contract;

public interface IBookRepository : IBookStatusRepository
{
    public Task<ulong> AddAsync(Book book, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Book book, CancellationToken cancellationToken = default);
    public Task DeleteAsync(ulong id, CancellationToken cancellationToken = default);
    public Task<Book?> GetAsync(ulong id, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Book>> GetManyAsync(string? title, string? author, string? isbn, string? status,
        int skip = 0, int take = 5, CancellationToken cancellationToken = default);
}