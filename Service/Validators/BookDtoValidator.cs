using FluentValidation;
using Service.Dtos;

namespace Service.Validators;

internal abstract class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(a => a).NotEmpty();
        RuleFor(r => r.Title).NotEmpty();
        RuleFor(r => r.Title).Length(1, 320).WithMessage("Title must be between 3 and 50 characters long.");
        RuleFor(r => r.Author).NotEmpty();
        RuleFor(r => r.Title).Length(3, 320).WithMessage("Title must be between 3 and 50 characters long.");
        RuleFor(r => r.Isbn).NotEmpty();
        RuleFor(r => r.Isbn).Length(17);
        RuleFor(r => r.BookStatus).NotEmpty();
    }
}