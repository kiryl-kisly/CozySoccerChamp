namespace CozySoccerChamp.Infrastructure.Repositories;

public class ApplicationUserRepository(DbContext context) : Repository<ApplicationUser>(context), IApplicationUserRepository;