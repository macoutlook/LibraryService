// using System.Threading.Tasks;
// using AutoMapper;
// using EntityFrameworkCore.Testing.Moq;
// using FluentAssertions;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Persistence.ArticleRepository;
// using Service;
//
// namespace Persistence.UnitTest;
//
// [TestClass]
// public class BookRepositoryUnitTest
// {
//     private IMapper _autoMapper;
//     private BookContext _dbContext;
//     private ArticleRepository.BookRepository _repository;
//
//     [TestInitialize]
//     public void SetUp()
//     {
//         var mapperConfig = new MapperConfigurationExpression();
//         mapperConfig.AddProfile(new MappingProfile());
//         _autoMapper = new MapperConfiguration(mapperConfig).CreateMapper();
//
//         _dbContext = Create.MockedDbContextFor<BookContext>();
//         _repository = new ArticleRepository.BookRepository(_dbContext, _autoMapper);
//
//         TestDataFeed.Create(_dbContext);
//     }
//
//     [TestMethod]
//     public async Task GetArticleAsync_ProperIdGiven_ArticleFound()
//     {
//         // Arrange
//         var id = 1;
//
//         // Act
//         var result = await _repository.GetArticleAsync(id).ConfigureAwait(false);
//
//         // Assert
//         result.Should().NotBeNull();
//         result?.Id.Should().Be(id);
//     }
//
//     [TestMethod]
//     public async Task GetArticleAsync_UnrecognizedIdGiven_ArticleNotFound()
//     {
//         // Arrange
//         var id = 5;
//
//         // Act
//         var result = await _repository.GetArticleAsync(id).ConfigureAwait(false);
//
//         // Assert
//         result.Should().BeNull();
//     }
// }