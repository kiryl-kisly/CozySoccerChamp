namespace CozySoccerChamp.Application.Services.Abstractions;

public interface ITelegramHandler
{
    Task HandleUpdateAsync(Update? update);
}