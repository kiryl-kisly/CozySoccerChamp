namespace CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;

public class PredictionEntityConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .HasOne(x => x.Match)
            .WithMany(x => x.Predictions)
            .HasForeignKey(x => x.MatchId);

        builder
            .HasOne(p => p.User)
            .WithMany(u => u.Predictions)
            .HasForeignKey(p => p.TelegramUserId)
            .HasPrincipalKey(u => u.TelegramUserId);
        
        builder
            .HasIndex(x => new { x.TelegramUserId, x.MatchId })
            .IsUnique();
    }
}