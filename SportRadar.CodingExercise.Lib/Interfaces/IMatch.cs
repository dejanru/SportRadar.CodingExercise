namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IMatch
    {
        ITeam homeTeam { get; set; }
        ITeam awayTeam { get; set; }
    }
}
