namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupHandler
    {
        ICollection<IMatch> StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();
        IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>> GetSummaryOfMatches();
        ICollection<IMatch> UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);
        Tuple<ICollection<IMatch>, ICollection<IMatch>> FinishMatch(string homeTeam, string awayTeam);
    }
}
