namespace CozySoccerChamp.Application.Models.Responses;

public class LeaderboardResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public double? Points { get; set; } = 0;
    public int Place { get; set; } = 0;
}