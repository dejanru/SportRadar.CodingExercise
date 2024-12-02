using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Services
{
    public class WorldCupServiceHandler : IWorldCupService
    {
        private readonly IWorldCupService _worldCupService;
        private ICollection<IMatch> _runningMatches;
        private ICollection<IMatch> _archiveMatches;
        public WorldCupServiceHandler(IWorldCupService worldCupService, IMatch match, ITeam team)
        {
            _worldCupService = worldCupService;
            _runningMatches = new List<IMatch>();
            _archiveMatches = new List<IMatch>();
        }

        public ICollection<IMatch> GetArchiveMatches()
        {
            return _worldCupService.GetArchiveMatches();
        }

        public ICollection<IMatch> GetRunningMatches()
        {
            return _runningMatches;
        }

        public IMatch StartNewMatch(string homeTeam, string awayTeam)
        {
            var match = _worldCupService.StartNewMatch(homeTeam, awayTeam);
            _runningMatches.Add(match);

            return match;
        }

        public IMatch UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = _runningMatches.FirstOrDefault(x => x.homeTeam.Name == homeTeam && x.awayTeam.Name == awayTeam);
            if (match == null)
            {
                throw new ArgumentException("No running match");
            }

            match.homeTeam.Score = homeScore;
            match.awayTeam.Score = awayScore;

            return match;
        }
    }
}
