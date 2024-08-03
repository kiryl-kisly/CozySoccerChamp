namespace CozySoccerChamp.Application.Models.Responses;

public record UserResponse
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
}