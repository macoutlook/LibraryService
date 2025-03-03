using AutoMapper;
using Core.Domain;
using Service.Dtos;

namespace Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookDto, Book>().ReverseMap();
        CreateMap<BookReadDto, Book>().ReverseMap();
        CreateMap<Book, Persistence.BookRepository.DbModels.Book>().ReverseMap();
    }
}

public class BookStatusConverter : IValueConverter<string, BookStatus> {
    public BookStatus Convert(string status, ResolutionContext context)
        => Enum.Parse<BookStatus>(status);
}

public class CustomResolver : IValueResolver<string, BookStatus, BookStatus>
{
    public BookStatus Resolve(string source, BookStatus destination, BookStatus member, ResolutionContext context)
    {
        return Enum.Parse<BookStatus>(source);
    }
}