namespace CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;

public class CompetitionEntityConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(x => x.Id);
    }
}