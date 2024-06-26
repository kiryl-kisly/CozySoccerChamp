namespace CozySoccerChamp.Infrastructure.Repositories;

public class PredictionRepository(DbContext context) : Repository<Prediction>(context), IPredictionRepository;