using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using Core.Application.Contract;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;

namespace Service.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller(
    ILogger<Controller> logger,
    IBookApplicationService bookApplicationService,
    IMapper mapper)
    : ControllerBase
{
    [HttpPost]
    [Route("/books")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> Add([FromBody] [Required] BookDto bookDto, CancellationToken cancellationToken = default)
    {
        var bookId = await bookApplicationService.AddBookAsync(mapper.Map<Book>(bookDto), cancellationToken);

        logger.LogInformation("Book with id={bookId} was created.", bookId);

        return Created($"/book/{bookId}", bookId);
    }

    [HttpPost]
    [Route("/books/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> Update(ulong id, [FromBody] [Required] BookDto bookDto, CancellationToken cancellationToken = default)
    {
        var bookDomainModel = new Book
        {
            Id = id,
            Author = bookDto.Author,
            Title = bookDto.Title,
            Isbn = bookDto.Isbn,
            BookStatus = bookDto.BookStatus
        };

        await bookApplicationService.UpdateBookAsync(bookDomainModel, cancellationToken);

        logger.LogInformation("Book with id={bookId} was updated.", id);

        return Ok();
    }

    [HttpPatch]
    [Route("/books/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> UpdateStatus(ulong id, [FromBody] BookStatus bookStatus, CancellationToken cancellationToken = default)
    {
        await bookApplicationService.UpdateStatusAsync(id, bookStatus, cancellationToken);

        logger.LogInformation("Book's status with id={bookId} was updated.", id);

        return Ok();
    }
    
    [HttpDelete]
    [Route("/books/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> Delete(ulong id, CancellationToken cancellationToken = default)
    {
        await bookApplicationService.DeleteBookAsync(id, cancellationToken);

        logger.LogInformation("Book's status with id={bookId} was deleted.", id);

        return Ok();
    }

    [HttpGet]
    [Route("books/{id}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<BookDto>> Get([Required] ulong id, CancellationToken cancellationToken = default)
    {
        var book = await bookApplicationService.GetBookAsync(id, cancellationToken);
        if (book == null) return NotFound();
    
        return Ok(mapper.Map<BookDto>(book));
    }
    
    [HttpGet]
    [Route("/books")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    public async Task<ActionResult<IEnumerable<BookDto>>> Get([FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? isbn, [FromQuery] BookStatus? status,
        [FromQuery] int skip = 0, [FromQuery] int take = 5, CancellationToken cancellationToken = default)
    {
        var books = await bookApplicationService.GetBooksAsync(title, author, isbn, status, skip, take, cancellationToken);
    
        return Ok(mapper.Map<List<BookDto>>(books));
    }
}