using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.Data.Configurations.User;

public class UserProfileEntityConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(up => up.TelegramUserId);
        
        builder.Property(x => x.TelegramUserId)
            .IsRequired();
        
        builder.HasIndex(x => x.TelegramUserId)
            .IsUnique();
        
        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Profile)
            .HasForeignKey<UserProfile>(x => x.TelegramUserId)
            .IsRequired();
    }
}