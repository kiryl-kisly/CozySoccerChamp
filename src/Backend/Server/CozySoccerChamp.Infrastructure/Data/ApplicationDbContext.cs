using CozySoccerChamp.Domain.Entities.User;
using CozySoccerChamp.Infrastructure.Data.Configurations.Soccer;
using CozySoccerChamp.Infrastructure.Data.Configurations.User;

namespace CozySoccerChamp.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<NotificationSettings> NotificationSettings => Set<NotificationSettings>();
    public DbSet<Competition> Competitions => Set<Competition>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<MatchResult> MatchResults => Set<MatchResult>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // User
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        builder.ApplyConfiguration(new UserProfileEntityConfiguration());
        builder.ApplyConfiguration(new NotificationSettingsEntityConfiguration());
        
        // Soccer
        builder.ApplyConfiguration(new CompetitionEntityConfiguration());
        builder.ApplyConfiguration(new MatchEntityConfiguration());
        builder.ApplyConfiguration(new MatchResultEntityConfiguration());
        builder.ApplyConfiguration(new TeamEntityConfiguration());
        builder.ApplyConfiguration(new PredictionEntityConfiguration());
    }
}