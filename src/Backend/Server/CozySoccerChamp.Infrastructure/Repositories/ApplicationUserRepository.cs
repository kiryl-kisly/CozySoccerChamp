using CozySoccerChamp.Domain.Entities.User;

namespace CozySoccerChamp.Infrastructure.Repositories;

public class ApplicationUserRepository(ApplicationDbContext context) : Repository<ApplicationUser>(context), IApplicationUserRepository;