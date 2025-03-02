using Core.Domain;

namespace Core.Persistence.Contract;

/// <summary>
///     Contract for repository to get and save article's data
/// </summary>
public interface IBookRepository
{
    public Task<ulong> AddAsync(Book book);

    public Task UpdateAsync(Book book);

    Task UpdateStatusAsync(ulong id, string status);
    //
    // /// <summary>
    // ///     Get particular article by it's id
    // /// </summary>
    // /// <param name="id">Identifier of article</param>
    // /// <returns>Domain model of article</returns>
    // public Task<Book?> GetArticleAsync(int id);
    //
    // /// <summary>
    // ///     Get all articles filtered by category
    // /// </summary>
    // /// <param name="category">Get all article by category, if null or empty, collection won't by filtered by category</param>
    // /// <param name="skip">Number of skipped elements in collection</param>
    // /// <param name="take">Number of taken articles</param>
    // /// <returns>Collection of filtered articles</returns>
    // public Task<IEnumerable<Book>> GetArticlesAsync(string? category, int skip = 0, int take = 5);
}