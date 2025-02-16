using CozySoccerChamp.Application.Models.Responses.Soccer;

namespace CozySoccerChamp.Application.Models.Responses;

public record Response(
    UserProfileResponse? UserProfile,
    CompetitionResponse? Competition,
    IReadOnlyCollection<MatchResponse>? Matches,
    IReadOnlyCollection<PredictionResponse>? Predictions,
    IReadOnlyCollection<LeaderboardResponse>? Leaderboard);