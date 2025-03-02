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
    [Route("/book")]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> Add([FromBody] [Required] BookDto bookDto)
    {
        var bookId = await bookApplicationService.AddBookAsync(mapper.Map<Book>(bookDto))
            .ConfigureAwait(false);

        logger.LogInformation("Book with id={bookId} was created.", bookId);

        return Created($"/book/{bookId}", bookId);
    }

    [HttpPost]
    [Route("/book/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> Update(ulong id, [FromBody] [Required] BookDto bookDto)
    {
        var bookDomainModel = new Book
        {
            Id = id,
            Author = bookDto.Author,
            Title = bookDto.Title,
            Isbn = bookDto.Isbn,
            Status = bookDto.Status
        };
        
        await bookApplicationService.UpdateBookAsync(bookDomainModel)
            .ConfigureAwait(false);

        logger.LogInformation("Book with id={bookId} was updated.", id);

        return Ok();
    }
    
    [HttpPatch]
    [Route("/book/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<string>> UpdateStatus(ulong id, [FromBody] Status status)
    {
        await bookApplicationService.UpdateStatusAsync(id, status)
            .ConfigureAwait(false);

        logger.LogInformation("Book's status with id={bookId} was updated.", id);

        return Ok();
    }

    // [HttpGet]
    // [Route("article/{Id}")]
    // [ProducesResponseType((int) HttpStatusCode.OK)]
    // [ProducesResponseType((int) HttpStatusCode.NotFound)]
    // [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    // public async Task<ActionResult<BookDto>> Get([Required] int Id)
    // {
    //     var article = await articleApplicationService.GetArticleAsync(Id).ConfigureAwait(false);
    //     if (article == null) return NotFound();
    //     var articleDto = mapper.Map<BookDto>(article);
    //
    //     return Ok(articleDto);
    // }
    //
    // [HttpGet]
    // [Route("/articles")]
    // [ProducesResponseType((int)HttpStatusCode.OK)]
    // [ProducesResponseType((int) HttpStatusCode.BadRequest)]
    // public async Task<ActionResult<IEnumerable<BookDto>>> GetAll([FromQuery] string? category,
    //     [FromQuery] int skip = 0, [FromQuery] int take = 5)
    // {
    //     var articles = await articleApplicationService.GetArticlesAsync(category, skip, take).ConfigureAwait(false);
    //     var articleDto = mapper.Map<List<BookDto>>(articles);
    //
    //     return Ok(articleDto);
    // }
}