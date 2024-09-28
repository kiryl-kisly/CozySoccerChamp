namespace CozySoccerChamp.Infrastructure.BackgroundServices.Jobs.Settings;

public class PointCalculateSettings
{
    public static readonly string SectionName = nameof(PointCalculateSettings);

    public PointSettings PointSettings { get; init; } = default!;
    public CoefficientSettings CoefficientSettings { get; init; } = default!;
}

public class PointSettings
{
    public int Outcome { get; init; }
    public int GoalDifference { get; init; }
    public int ExactScore { get; init; }
}

public class CoefficientSettings
{
    public double LeagueStage { get; init; }
    public double Last16 { get; init; }
    public double QuarterFinals { get; init; }
    public double SemiFinals { get; init; }
    public double Final { get; init; }
}