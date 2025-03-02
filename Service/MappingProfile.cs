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
        CreateMap<Book, Persistence.ArticleRepository.DbModels.Book>().ReverseMap();
    }
}