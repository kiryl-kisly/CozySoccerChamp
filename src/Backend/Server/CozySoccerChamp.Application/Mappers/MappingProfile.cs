using CozySoccerChamp.Application.Models.Requests.Prediction;
using CozySoccerChamp.Application.Models.Responses.Soccer;
using CozySoccerChamp.Domain.Entities.Soccer;

namespace CozySoccerChamp.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>
                src.UserName
                ?? (string.IsNullOrEmpty(src.TelegramUserName)
                    ? $"{src.TelegramUserName} {src.TelegramLastName}"
                    : src.TelegramUserName)))
            .ReverseMap();

        CreateMap<Match, MatchResponse>()
            .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StartTimeUtc, opt => opt.MapFrom(src => src.MatchTime))
            .ForMember(dest => dest.TeamHome, opt => opt.MapFrom(src => src.TeamHome))
            .ForMember(dest => dest.TeamAway, opt => opt.MapFrom(src => src.TeamAway))
            .ForMember(dest => dest.MatchResult, opt => opt.MapFrom(src => src.MatchResult))
            .ReverseMap();

        CreateMap<Team, TeamResponse>()
            .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<MatchResult, MatchResultResponse>()
            .ForMember(dest => dest.MatchResultId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullTime, opt => opt.MapFrom(src => GetScore(src.FullTime)))
            .ForMember(dest => dest.HalfTime, opt => opt.MapFrom(src => GetScore(src.HalfTime)))
            .ForMember(dest => dest.RegularTime, opt => opt.MapFrom(src => GetScore(src.RegularTime)))
            .ForMember(dest => dest.ExtraTime, opt => opt.MapFrom(src => GetScore(src.ExtraTime)))
            .ForMember(dest => dest.Penalties, opt => opt.MapFrom(src => GetScore(src.Penalties)))
            .ReverseMap();
        
        CreateMap<Prediction, PredictionRequest>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.MatchId))
            .ForPath(dest => dest.Prediction!.PredictedHomeScore, opt => opt.MapFrom(src => src.PredictedHomeScore))
            .ForPath(dest => dest.Prediction!.PredictedAwayScore, opt => opt.MapFrom(src => src.PredictedAwayScore))
            .ReverseMap();
    }

    private static ScoreResponse? GetScore(string? source)
    {
        if (string.IsNullOrEmpty(source))
            return null;

        var splitValue = source.Split(':');

        return new ScoreResponse
        {
            HomeTeamScore = Convert.ToInt32(splitValue[0]),
            AwayTeamScore = Convert.ToInt32(splitValue[1])
        };
    }
}