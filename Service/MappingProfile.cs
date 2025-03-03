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
        CreateMap<Book, Persistence.BookRepository.DbModels.Book>();;
        CreateMap<Persistence.BookRepository.DbModels.Book, Book>().ForPath(dst => dst.BookStatus, 
            src => src.MapFrom(s => s.Status));
    }
}