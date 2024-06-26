namespace CozySoccerChamp.Infrastructure.Repositories;

public class MatchResultRepository(DbContext context) : Repository<MatchResult>(context), IMatchResultRepository;