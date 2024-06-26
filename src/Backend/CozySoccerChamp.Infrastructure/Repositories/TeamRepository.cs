namespace CozySoccerChamp.Infrastructure.Repositories;

public class TeamRepository(DbContext context) : Repository<Team>(context), ITeamRepository;