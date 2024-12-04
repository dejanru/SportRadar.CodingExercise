namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupService
    {
        IMatch StartNewMatch(string homeTeam, string awayTeam);
        ICollection<IMatch> GetRunningMatches();
        ICollection<IMatch> GetArchiveMatches();

        IMatch UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);

        IMatch FinishMatch(string homeTeam, string awayTeam);
    }
}
