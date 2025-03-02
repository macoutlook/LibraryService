using Core.Application.Contract;
using Core.Domain;
using Core.Persistence.Contract;

namespace Core.Application
{
    public class BookApplicationService : IBookApplicationService
    {
        private readonly IBookRepository _repository;

        public BookApplicationService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddBookAsync(Book book)
        {
            return await _repository.SaveBookAsync(book).ConfigureAwait(false);
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
}