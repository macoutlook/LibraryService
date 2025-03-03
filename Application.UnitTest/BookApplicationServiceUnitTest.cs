using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Application;
using Core.Application.DomainServices;
using Core.Domain;
using Core.Persistence.Contract;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Application.UnitTest;

public class BookApplicationServiceTests
{
    private readonly IBookRepository _repository;
    private readonly IBookStateMachineValidator _validator;
    private readonly BookApplicationService _service;

    public BookApplicationServiceTests()
    {
        _repository = Substitute.For<IBookRepository>();
        _validator = Substitute.For<IBookStateMachineValidator>();
        _service = new BookApplicationService(_repository, _validator);
    }

    [Fact]
    public async Task AddBookAsync_ValidBook_ReturnsBookId()
    {
        // Arrange
        var book = new Book { Title = "Test Book", Author = "Test Author" };
        var bookId = 1UL;

        _repository.AddAsync(book).Returns(bookId);

        // Act
        var result = await _service.AddBookAsync(book);

        // Assert
        result.Should().Be(bookId);
    }

    [Fact]
    public async Task UpdateBookAsync_ValidBook_UpdatesBook()
    {
        // Arrange
        var book = new Book { Id = 1, Title = "Test Book", Author = "Test Author" };

        _validator.ValidateAsync(Arg.Any<ulong>(), Arg.Any<BookStatus>()).Returns(Task.CompletedTask);
        
        // Act
        await _service.UpdateBookAsync(book);

        // Assert
        await _repository.Received(1).UpdateAsync(book);
    }

    [Fact]
    public async Task UpdateStatusAsync_ValidBook_UpdatesBookStatus()
    {
        // Arrange
        var bookId = 1UL;
        var bookStatus = BookStatus.OnShelf;

        // Act
        await _service.UpdateStatusAsync(bookId, bookStatus);

        // Assert
        await _repository.Received(1).UpdateStatusAsync(bookId, bookStatus.ToString());
    }

    [Fact]
    public async Task DeleteBookAsync_ValidBookId_DeletesBook()
    {
        // Arrange
        var bookId = 1UL;

        // Act
        await _service.DeleteBookAsync(bookId);

        // Assert
        await _repository.Received(1).DeleteAsync(bookId);
    }

    [Fact]
    public async Task GetBookAsync_ValidBookId_ReturnsBook()
    {
        // Arrange
        var bookId = 1UL;
        var book = new Book { Id = bookId, Title = "Test Book", Author = "Test Author" };

        _repository.GetAsync(bookId).Returns(book);

        // Act
        var result = await _service.GetBookAsync(bookId);

        // Assert
        result.Should().BeEquivalentTo(book);
    }

    [Fact]
    public async Task GetBooksAsync_AuthorFilter_ReturnsExpectedBooks()
    {
        // Arrange
        var books = new[]
        {
            new Book { Id = 1, Title = "Test Book 1", Author = "Test Author 1" },
            new Book { Id = 2, Title = "Test Book 2", Author = "Test Author 1" }
        };
        _repository.GetManyAsync(Arg.Any<string>(), Arg.Any<string>(),Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()).Returns(books);

        // Act
        var result = await _service.GetBooksAsync(null, "Test Author 1", null, null);

        // Assert
        result.Should().BeEquivalentTo(books);
    }
}