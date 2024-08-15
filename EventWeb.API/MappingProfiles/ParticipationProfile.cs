using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Models;

public class ParticipationProfile : Profile
{
    public ParticipationProfile()
    {
        CreateMap<ParticipationRequestDTO, Participation>()
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.EventRegistrationDate, opt => opt.MapFrom(src => DateOnly.Parse(src.EventRegistrationDate)));

        CreateMap<Participation, ParticipationResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
            .ForMember(dest => dest.EventRegistrationDate, opt => opt.MapFrom(src => src.EventRegistrationDate.ToString()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
    }
}