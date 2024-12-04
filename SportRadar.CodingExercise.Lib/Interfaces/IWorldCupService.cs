namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupService
    {
        ICollection<IMatch> StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();

        ICollection<IMatch> UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);

        Tuple<ICollection<IMatch>, ICollection<IMatch>> FinishMatch(string homeTeam, string awayTeam);
    }
}
