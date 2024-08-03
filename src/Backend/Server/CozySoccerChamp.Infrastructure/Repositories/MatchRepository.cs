namespace CozySoccerChamp.Infrastructure.Repositories;

public class MatchRepository(ApplicationDbContext context) : Repository<Match>(context), IMatchRepository;