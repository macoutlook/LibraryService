using AutoMapper;
using Service.Dtos;

namespace Service;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookDto, Core.Domain.Book>().ReverseMap();
        CreateMap<Core.Domain.Book, Persistence.ArticleRepository.DbModels.Book>().ReverseMap();
    }
}