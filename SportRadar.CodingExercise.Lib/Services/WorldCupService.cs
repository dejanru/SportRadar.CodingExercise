using SportRadar.CodingExercise.Lib.Interfaces;
using SportRadar.CodingExercise.Lib.Models;

namespace SportRadar.CodingExercise.Lib.Services
{
    public class WorldCupService : IWorldCupService
    {
        private ICollection<IMatch> _runningMatches;
        private ICollection<IMatch> _archiveMatches;

        public WorldCupService()
        {
            _runningMatches = new List<IMatch>();
            _archiveMatches = new List<IMatch>();
        }

        public IMatch FinishMatch(string homeTeam, string awayTeam)
        {
            var match = _runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);
            _runningMatches.Remove(match);
            _archiveMatches.Add(match);

            return match;
        }

        public ICollection<IMatch> GetArchiveMatches()
        {
            return _archiveMatches;
        }

        public ICollection<IMatch> GetRunningMatches()
        {
            return _runningMatches;
        }

        public IMatch StartNewMatch(string homeTeam, string awayTeam)
        {
            var match = new Match(homeTeam, awayTeam);
            _runningMatches.Add(match);

            return match;
        }

        public IMatch UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = _runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);
            if (match == null)
            {
                throw new ArgumentException("No running match");
            }

            match.HomeTeam.Score = homeScore;
            match.AwayTeam.Score = awayScore;

            return match;
        }
    }
}
