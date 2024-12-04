namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupHandler
    {
        IMatch StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();
        IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>> GetSummaryOfMatches();
        IMatch FinishMatch(string homeTeam, string awayTeam);
    }
}
