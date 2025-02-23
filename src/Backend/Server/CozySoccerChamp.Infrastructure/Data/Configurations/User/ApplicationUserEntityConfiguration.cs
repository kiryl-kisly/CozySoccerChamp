using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.Data.Configurations.User;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(u => u.TelegramUserId);

        builder.Property(x => x.TelegramUserId)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasMaxLength(50);

        builder.HasOne(x => x.Profile)
            .WithOne(x => x.User)
            .HasForeignKey<UserProfile>(up => up.TelegramUserId)
            .HasPrincipalKey<ApplicationUser>(u => u.TelegramUserId)
            .IsRequired();
    }
}