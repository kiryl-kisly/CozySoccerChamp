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
            .ForMember(dest => dest.TelegramUserName, opt => opt.MapFrom(src => src.Username ?? string.Empty))
            .ForMember(dest => dest.TelegramFirstName, opt => opt.MapFrom(src => src.FirstName ?? string.Empty))
            .ForMember(dest => dest.TelegramLastName, opt => opt.MapFrom(src => src.LastName ?? string.Empty));

        CreateMap<TeamResponse, Team>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalTeamId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.ShortName))
            .ForMember(dest => dest.CodeName, opt => opt.MapFrom(src => src.CodeName))
            .ForMember(dest => dest.EmblemUrl, opt => opt.MapFrom(src => src.EmblemUrl));

        CreateMap<MatchResponse, Match>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalMatchId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Group, opt => opt.MapFrom(src =>
                string.IsNullOrEmpty(src.Group) ? (char?)null : src.Group.Replace("GROUP_", "").ToCharArray()[0]))
            .ForMember(dest => dest.Stage, opt => opt.MapFrom(src => src.Stage))
            .ForMember(dest => dest.MatchDay, opt => opt.MapFrom(src => src.Matchday))
            .ForMember(dest => dest.MatchTime, opt => opt.MapFrom(src => src.StartDateUtc))
            .ForMember(dest => dest.Competition, opt => opt.Ignore())
            .ForMember(dest => dest.MatchResult, opt => opt.Ignore());

        CreateMap<CompetitionResponse, Competition>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.EmblemUrl, opt => opt.MapFrom(src => src.EmblemUrl));
    }
}