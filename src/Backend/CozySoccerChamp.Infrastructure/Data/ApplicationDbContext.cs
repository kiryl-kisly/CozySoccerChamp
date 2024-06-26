namespace CozySoccerChamp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<Championship> Championships => Set<Championship>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchResult> Results => Set<MatchResult>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        new ApplicationUserEntityConfiguration().Configure(builder.Entity<ApplicationUser>());

        new ChampionshipEntityConfiguration().Configure(builder.Entity<Championship>());
        new MatchEntityConfiguration().Configure(builder.Entity<Match>());
        new MatchResultEntityConfiguration().Configure(builder.Entity<MatchResult>());
        new TeamEntityConfiguration().Configure(builder.Entity<Team>());
        new PredictionEntityConfiguration().Configure(builder.Entity<Prediction>());
    }
}