using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.Data.Configurations.User;

public class NotificationSettingsEntityConfiguration : IEntityTypeConfiguration<NotificationSettings>
{
    public void Configure(EntityTypeBuilder<NotificationSettings> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TelegramUserId)
            .IsRequired();

        builder.HasOne(ns => ns.UserProfile)
            .WithOne(up => up.NotificationSettings)
            .HasForeignKey<NotificationSettings>(ns => ns.TelegramUserId)
            .HasPrincipalKey<UserProfile>(up => up.TelegramUserId)
            .IsRequired(false);

        builder.Property(x => x.IsAnnouncementEnabled)
            .IsRequired();

        builder.Property(x => x.IsReminderEnabled)
            .IsRequired();

        builder.Property(x => x.ReminderInterval)
            .IsRequired();

        builder.Property(x => x.IsForceNotificationEnabled)
            .IsRequired();
        
        builder.Property(x => x.LastReminderNotified)
            .IsRequired(false);
    }
}