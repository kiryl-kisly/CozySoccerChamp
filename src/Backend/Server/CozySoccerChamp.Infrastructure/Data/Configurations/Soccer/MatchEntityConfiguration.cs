namespace CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;

public class MatchEntityConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MatchTime)
            .IsRequired();

        builder.Property(x => x.Group)
            .HasMaxLength(1);

        builder
            .HasOne(x => x.TeamHome)
            .WithMany(x => x.HomeMatches)
            .HasForeignKey(x => x.TeamHomeId);

        builder
            .HasOne(x => x.TeamAway)
            .WithMany(x => x.AwayMatches)
            .HasForeignKey(x => x.TeamAwayId);

        builder
            .HasOne(x => x.MatchResult)
            .WithOne(x => x.Match)
            .HasForeignKey<MatchResult>(x => x.MatchId)
            .IsRequired();

        builder
            .HasOne(x => x.Competition)
            .WithMany(x => x.Matches)
            .HasForeignKey(x => x.CompetitionId);
    }
}