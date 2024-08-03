namespace CozySoccerChamp.Infrastructure.Repositories;

public class MatchResultRepository(ApplicationDbContext context) : Repository<MatchResult>(context), IMatchResultRepository;