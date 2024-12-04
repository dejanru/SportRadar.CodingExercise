using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Services
{
    public class WorldCupHandler : IWorldCupHandler
    {

        private IWorldCupService _worldCupService;
        public WorldCupHandler(IWorldCupService worldCupService, IMatch match, ITeam team)
        {
            _worldCupService = worldCupService;
        }

        public IMatch FinishMatch(string homeTeam, string awayTeam)
        {
            var match = _worldCupService.FinishMatch(homeTeam, awayTeam);

            return match;
        }

        public ICollection<IMatch> GetArchiveMatches()
        {
            return _worldCupService.GetArchiveMatches();
        }

        public ICollection<IMatch> GetRunningMatches()
        {
            return _worldCupService.GetRunningMatches();
        }

        public IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>> GetSummaryOfMatches()
        {
            var runningMatches = _worldCupService.GetRunningMatches().OrderByDescending(o => o.CreatedTicks);
            SortedList<Tuple<int, long>, IMatch> keyValuePairs = new SortedList<Tuple<int, long>, IMatch>();
            int pos = 0;
            foreach (var match in runningMatches)
            {
                pos++;
                keyValuePairs.Add(Tuple.Create(item1: match.AwayTeam.Score + match.HomeTeam.Score, item2: match.CreatedTicks), match);
            }

            return keyValuePairs.OrderByDescending(k => k.Key);
        }

        public IMatch StartNewMatch(string homeTeam, string awayTeam)
        {
            var match = _worldCupService.StartNewMatch(homeTeam, awayTeam);

            return match;
        }

        public IMatch UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = _worldCupService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            return match;
        }
    }
}
