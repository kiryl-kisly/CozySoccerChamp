using CozySoccerChamp.Application.Models.Requests.Prediction;
using CozySoccerChamp.Application.Models.Responses.Soccer;
using CozySoccerChamp.Domain.Entities.Soccer;

namespace CozySoccerChamp.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, UserResponse>()
            .ForMember(dest => dest.TelegramUserId, opt => opt.MapFrom(src => src.TelegramUserId))
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
            .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.MatchId))
            .ForPath(dest => dest.Prediction!.PredictedHomeScore, opt => opt.MapFrom(src => src.PredictedHomeScore))
            .ForPath(dest => dest.Prediction!.PredictedAwayScore, opt => opt.MapFrom(src => src.PredictedAwayScore))
            .ReverseMap();
        
        CreateMap<Prediction, PredictionResponse>()
            .ForMember(dest => dest.MatchId, opt => opt.MapFrom(src => src.MatchId))
            .ForMember(dest => dest.PredictedHomeScore, opt => opt.MapFrom(src => src.PredictedHomeScore))
            .ForMember(dest => dest.PredictedAwayScore, opt => opt.MapFrom(src => src.PredictedAwayScore))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();
        
        CreateMap<Competition, CompetitionResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.EmblemUrl, opt => opt.MapFrom(src => src.EmblemUrl))
            .ForMember(dest => dest.Started, opt => opt.MapFrom(src => src.Started))
            .ForMember(dest => dest.Finished, opt => opt.MapFrom(src => src.Finished))
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