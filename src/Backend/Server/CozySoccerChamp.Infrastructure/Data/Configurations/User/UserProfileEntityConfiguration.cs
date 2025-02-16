using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.Data.Configurations.User;

public class UserProfileEntityConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TelegramUserId)
            .IsRequired();

        builder.HasOne(up => up.User)
            .WithOne(u => u.Profile)
            .HasForeignKey<UserProfile>(up => up.TelegramUserId)
            .HasPrincipalKey<ApplicationUser>(u => u.TelegramUserId)
            .IsRequired();

        builder.HasOne(up => up.NotificationSettings)
            .WithOne(ns => ns.UserProfile)
            .HasForeignKey<NotificationSettings>(ns => ns.TelegramUserId)
            .IsRequired(false);
    }
}