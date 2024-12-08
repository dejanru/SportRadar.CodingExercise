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

        public ICollection<IMatch> GetArchiveMatches()
        {
            return _archiveMatches;
        }

        public ICollection<IMatch> GetRunningMatches()
        {
            return _runningMatches;
        }

        public ICollection<IMatch> StartNewMatch(string homeTeam, string awayTeam)
        {
            try
            {
                bool matchAlreadyExist = _runningMatches.Any(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);

                bool homeTeamAlreadyPlays = _runningMatches.Any(x => x.HomeTeam.Name == homeTeam || x.AwayTeam.Name == homeTeam);
                bool awayTeamAlreadyPlays = _runningMatches.Any(x => x.HomeTeam.Name == awayTeam || x.AwayTeam.Name == awayTeam);

                if (!matchAlreadyExist)
                {
                    if (homeTeamAlreadyPlays)
                    {
                        throw new Exception($"Team ({homeTeam}) already plays match in progress.");
                    }

                    if (awayTeamAlreadyPlays)
                    {
                        throw new Exception($"Team ({awayTeam}) already plays match in progress.");
                    }
                    var match = new Match(homeTeam, awayTeam);
                    _runningMatches.Add(match);
                }
                else
                {
                    throw new Exception($"Match between teams : Home ({homeTeam}) and away: ({awayTeam})already in progress.");
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _runningMatches;
        }

        public ICollection<IMatch> UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = _runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);
            if (match == null)
            {
                throw new ArgumentException($"No running match between {homeTeam} and {awayTeam}");
            }

            match.HomeTeam.Score = homeScore;
            match.AwayTeam.Score = awayScore;

            return _runningMatches;
        }

        public Tuple<ICollection<IMatch>, ICollection<IMatch>> FinishMatch(string homeTeam, string awayTeam)
        {
            var match = _runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);
            if (match == null)
            {
                throw new ArgumentException($"No running match between {homeTeam} and {awayTeam}");
            }
            _runningMatches.Remove(match);
            _archiveMatches.Add(match);

            return Tuple.Create(_runningMatches, _archiveMatches);
        }
    }
}
