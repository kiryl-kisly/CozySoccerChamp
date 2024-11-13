namespace CozySoccerChamp.Infrastructure.Data.Configurations;

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
    }
}