namespace CozySoccerChamp.Infrastructure.Repositories;

public class PredictionRepository(ApplicationDbContext context) : Repository<Prediction>(context), IPredictionRepository;