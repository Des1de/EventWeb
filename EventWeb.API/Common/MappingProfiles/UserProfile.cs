using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegistrationRequestDTO, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDay)))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src=> src.Name + "." + src.Surname));

        CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay.ToString()));
    }
}