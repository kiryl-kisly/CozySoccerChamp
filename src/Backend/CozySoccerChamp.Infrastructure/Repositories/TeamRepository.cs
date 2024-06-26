namespace CozySoccerChamp.Infrastructure.Repositories;

public class TeamRepository(ApplicationDbContext context) : Repository<Team>(context), ITeamRepository;