using CozySoccerChamp.Domain.Entities.Soccer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozySoccerChamp.Infrastructure.Data.Configurations;

public class PredictionEntityConfiguration : IEntityTypeConfiguration<Prediction>
{
    public void Configure(EntityTypeBuilder<Prediction> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Predictions)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Match)
            .WithMany(x => x.Predictions)
            .HasForeignKey(x => x.MatchId);
    }
}