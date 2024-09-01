using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Models;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryRequestDTO, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Category, CategoryResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}