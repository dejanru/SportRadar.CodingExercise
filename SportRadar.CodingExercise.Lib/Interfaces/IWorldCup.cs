namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupHandler
    {
        IMatch StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();
    }
}
