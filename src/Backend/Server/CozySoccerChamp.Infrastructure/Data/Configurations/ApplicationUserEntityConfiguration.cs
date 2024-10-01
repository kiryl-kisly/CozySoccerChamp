namespace CozySoccerChamp.Infrastructure.Data.Configurations;

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.TelegramUserId);

        builder.Property(x => x.TelegramUserId)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasMaxLength(50);

        builder.HasIndex(x => x.TelegramUserId)
            .IsUnique();
    }
}