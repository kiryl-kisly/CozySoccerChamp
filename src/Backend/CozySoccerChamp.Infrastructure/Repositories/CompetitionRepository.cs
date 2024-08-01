namespace CozySoccerChamp.Infrastructure.Repositories;

public class CompetitionRepository(ApplicationDbContext context) : Repository<Competition>(context), ICompetitionRepository;