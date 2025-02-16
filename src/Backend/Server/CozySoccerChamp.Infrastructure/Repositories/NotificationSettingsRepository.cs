using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.Repositories;

public class NotificationSettingsRepository(ApplicationDbContext context) : Repository<NotificationSettings>(context), INotificationSettingsRepository;