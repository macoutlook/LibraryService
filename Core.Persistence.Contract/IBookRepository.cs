using Core.Domain;

namespace Core.Persistence.Contract;

public interface IBookRepository : IBookStatusRepository
{
    public Task<ulong> AddAsync(Book book);
    public Task UpdateAsync(Book book);
    public Task DeleteAsync(ulong id);
    public Task<Book?> GetAsync(ulong id);
    public Task<IReadOnlyList<Book>> GetManyAsync(string? title, string? author, string? isbn, string? status, int skip = 0, int take = 5);
}