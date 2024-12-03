using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Services
{
    public class WorldCupService : IWorldCupService
    {
        public WorldCupService() { }

        public ICollection<IMatch> GetArchiveMatches()
        {
            throw new NotImplementedException();
        }

        public ICollection<IMatch> GetRunningMatches()
        {
            throw new NotImplementedException();
        }

        public IMatch StartNewMatch(string homeTeam, string awayTeam)
        {
            throw new NotImplementedException();
        }

        public IMatch UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            throw new NotImplementedException();
        }
    }
}
