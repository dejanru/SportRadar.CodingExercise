namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupHandler
    {
        ICollection<IMatch> StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();
        IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>> GetSummaryOfMatches();
        Tuple<ICollection<IMatch>, ICollection<IMatch>> FinishMatch(string homeTeam, string awayTeam);
    }
}
