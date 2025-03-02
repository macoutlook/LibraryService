using FluentValidation;
using Service.Dtos;

namespace Service.Validators;

internal class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(a => a).NotEmpty();
        RuleFor(r => r.Category).NotEmpty();
        RuleFor(r => r.Content).NotEmpty();
        RuleFor(r => r.Title).NotEmpty();
    }
}