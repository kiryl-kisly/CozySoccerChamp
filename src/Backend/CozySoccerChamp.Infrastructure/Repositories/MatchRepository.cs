namespace CozySoccerChamp.Infrastructure.Repositories;

public class MatchRepository(DbContext context) : Repository<Match>(context), IMatchRepository;