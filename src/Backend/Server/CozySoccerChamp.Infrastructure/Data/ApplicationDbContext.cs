using CozySoccerChamp.Domain.Entities.User;
using CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;
using CozySoccerChamp.Infrastructure.Data.Configurations.User;

namespace CozySoccerChamp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Competition> Competitions => Set<Competition>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchResult> MatchResults => Set<MatchResult>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // User
        new ApplicationUserEntityConfiguration().Configure(builder.Entity<ApplicationUser>());
        new UserProfileEntityConfiguration().Configure(builder.Entity<UserProfile>());

        // Soccer
        new CompetitionEntityConfiguration().Configure(builder.Entity<Competition>());
        new MatchEntityConfiguration().Configure(builder.Entity<Match>());
        new MatchResultEntityConfiguration().Configure(builder.Entity<MatchResult>());
        new TeamEntityConfiguration().Configure(builder.Entity<Team>());
        new PredictionEntityConfiguration().Configure(builder.Entity<Prediction>());
    }
}