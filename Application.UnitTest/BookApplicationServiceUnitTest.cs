// using System;
// using System.Threading.Tasks;
// using Core.Application;
// using Core.Domain;
// using Core.Persistence.Contract;
// using FluentAssertions;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
//
// namespace Application.UnitTest;
//
// [TestClass]
// public class ArticleRepositoryUnitTest
// {
//     private BookApplicationService _applicationService;
//     private Mock<IArticleRepository> _repositoryMock;
//
//     [TestInitialize]
//     public void SetUp()
//     {
//         _repositoryMock = new Mock<IArticleRepository>();
//         _applicationService = new BookApplicationService(_repositoryMock.Object);
//     }
//
//     [TestMethod]
//     public async Task GetArticleAsync_ProperIdGiven_ArticleFound()
//     {
//         // Arrange
//         var id = 1;
//         var expectedArticle = new Article
//         {
//             Id = id,
//             Category = "Category",
//             Content = "Content",
//             Title = "Title"
//         };
//         _repositoryMock.Setup(m => m.GetArticleAsync(It.Is<int>(s => s.Equals(id))))
//             .Returns(Task.FromResult(expectedArticle));
//
//         // Act
//         var result = await _applicationService.GetArticleAsync(id).ConfigureAwait(false);
//
//         // Assert
//         result.Should().NotBeNull();
//         result?.Id.Should().Be(id);
//     }
//
//     [TestMethod]
//     public void GetArticleAsync_UnexpectedExceptionFromRepository_ExceptionThrown()
//     {
//         // Arrange
//         var id = 5;
//         var expectedExceptionMessage = "UnexpectedBehaviourException";
//         _repositoryMock.Setup(m => m.GetArticleAsync(It.Is<int>(s => s.Equals(id))))
//             .ThrowsAsync(new Exception(expectedExceptionMessage));
//
//         // Act
//         Func<Task> func = async () => await _applicationService.GetArticleAsync(id).ConfigureAwait(false);
//
//         // Assert
//         func.Should().Throw<Exception>().WithMessage(expectedExceptionMessage);
//     }
// }

