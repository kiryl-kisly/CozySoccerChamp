using AutoMapper;
using CozySoccerChamp.External.SoccerApi.Models.Responses;
using Telegram.Bot.Types;

namespace CozySoccerChamp.Infrastructure.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Chat, ApplicationUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.TelegramUsername, opt => opt.MapFrom(src => src.Username ?? string.Empty))
            .ForMember(dest => dest.TelegramFirstName, opt => opt.MapFrom(src => src.FirstName ?? string.Empty))
            .ForMember(dest => dest.TelegramLastName, opt => opt.MapFrom(src => src.LastName ?? string.Empty));

        CreateMap<TeamResponse, Team>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ShortName))
            .ForMember(dest => dest.CodeName, opt => opt.MapFrom(src => src.CodeName));
    }
}