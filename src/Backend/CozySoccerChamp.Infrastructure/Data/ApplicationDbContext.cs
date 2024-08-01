using CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;

namespace CozySoccerChamp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<Competition> Competitions => Set<Competition>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchResult> MatchResults => Set<MatchResult>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        new ApplicationUserEntityConfiguration().Configure(builder.Entity<ApplicationUser>());

        new CompetitionEntityConfiguration().Configure(builder.Entity<Competition>());
        new MatchEntityConfiguration().Configure(builder.Entity<Match>());
        new MatchResultEntityConfiguration().Configure(builder.Entity<MatchResult>());
        new TeamEntityConfiguration().Configure(builder.Entity<Team>());
        new PredictionEntityConfiguration().Configure(builder.Entity<Prediction>());
    }
}