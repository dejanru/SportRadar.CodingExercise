namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCup
    {
        IMatch StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();
    }
}
