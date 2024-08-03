namespace CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;

public class MatchResultEntityConfiguration : IEntityTypeConfiguration<MatchResult>
{
    public void Configure(EntityTypeBuilder<MatchResult> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (MatchResultStatus)Enum.Parse(typeof(MatchResultStatus), v));

        builder
            .Property(x => x.Duration)
            .HasConversion(
                v => v.ToString(),
                v => (DurationStatus)Enum.Parse(typeof(DurationStatus), v));

        builder
            .HasOne(x => x.Match)
            .WithOne(x => x.MatchResult)
            .HasForeignKey<MatchResult>(x => x.MatchId)
            .IsRequired();
    }
}