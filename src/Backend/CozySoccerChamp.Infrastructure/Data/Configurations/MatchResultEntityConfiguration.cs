using CozySoccerChamp.Domain.Entities.Soccer;
using CozySoccerChamp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozySoccerChamp.Infrastructure.Data.Configurations;

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
            .HasOne(x => x.Match)
            .WithOne(x => x.MatchResult)
            .HasForeignKey<MatchResult>(x => x.MatchId)
            .IsRequired();
    }
}