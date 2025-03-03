using Core.Domain;

namespace Core.Application.Contract;

public interface IBookApplicationService
{
    public Task<ulong> AddBookAsync(Book book);
    public Task UpdateBookAsync(Book book);
    public Task UpdateStatusAsync(ulong id, BookStatus bookStatus);
    public Task DeleteBookAsync(ulong id);
    public Task<Book?> GetBookAsync(ulong id);
    public Task<IReadOnlyList<Book>> GetBooksAsync(string? title,  string? author,  string? isbn,  BookStatus? status, int skip = 0, int take = 5);
}