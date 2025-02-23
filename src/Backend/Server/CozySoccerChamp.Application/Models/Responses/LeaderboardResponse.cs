namespace CozySoccerChamp.Application.Models.Responses;

public record LeaderboardResponse
{
    public long TelegramUserId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public double? Points { get; init; } = 0;
    public int Place { get; init; }
}