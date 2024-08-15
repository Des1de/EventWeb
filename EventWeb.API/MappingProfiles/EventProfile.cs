using AutoMapper;
using EventWeb.API.DTOs;
using EventWeb.Core.Models;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventRequestDTO, Event>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.EventTime, opt => opt.MapFrom(src => DateTime.Parse(src.EventTime)))
            .ForMember(dest => dest.MaxParticipantsNumber, opt => opt.MapFrom(src => src.MaxParticipantsNumber));

        CreateMap<Event, EventResponseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.EventTime, opt => opt.MapFrom(src => src.EventTime.ToString()))
            .ForMember(dest => dest.MaxParticipantsNumber, opt => opt.MapFrom(src => src.MaxParticipantsNumber));
    }
}