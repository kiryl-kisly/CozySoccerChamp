namespace CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;

public class PredictionEntityConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Predictions)
            .HasForeignKey(x => x.TelegramUserId);

        builder
            .HasOne(x => x.Match)
            .WithMany(x => x.Predictions)
            .HasForeignKey(x => x.MatchId);
        
        builder
            .HasIndex(x => new { x.TelegramUserId, x.MatchId })
            .IsUnique();
    }
}