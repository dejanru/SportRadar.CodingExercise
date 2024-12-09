using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Services
{
    public class WorldCupHandler : IWorldCupHandler
    {
        private IWorldCupService _worldCupService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldCupHandler"/> class.
        /// </summary>
        /// <param name="worldCupService">The world cup service.</param>
        /// <param name="match">The match.</param>
        /// <param name="team">The team.</param>
        public WorldCupHandler(IWorldCupService worldCupService, IMatch match, ITeam team)
        {
            _worldCupService = worldCupService;
        }


        public async Task<ICollection<IMatch>> GetArchiveMatches()
        {
            var res = await _worldCupService.GetArchiveMatches();

            return res;
        }

        public async Task<ICollection<IMatch>> GetRunningMatches()
        {
            return await _worldCupService.GetRunningMatches();
        }

        public async Task<ICollection<IMatch>> StartNewMatch(string homeTeam, string awayTeam)
        {
            var match = await _worldCupService.StartNewMatch(homeTeam, awayTeam);

            return match;
        }

        public async Task<ICollection<IMatch>> UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var match = await _worldCupService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            return match;
        }

        public async Task<Tuple<ICollection<IMatch>, ICollection<IMatch>>> FinishMatch(string homeTeam, string awayTeam)
        {
            var lists = await _worldCupService.FinishMatch(homeTeam, awayTeam);

            return lists;
        }

        public async Task<IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>>> GetSummaryOfMatches()
        {
            var runningMatches = await _worldCupService.GetRunningMatches();
            var runningMatches_ordered = runningMatches.OrderByDescending(o => o.CreatedTicks);
            SortedList<Tuple<int, long>, IMatch> keyValuePairs = new SortedList<Tuple<int, long>, IMatch>();
            foreach (var match in runningMatches_ordered)
            {
                keyValuePairs.Add(Tuple.Create(item1: match.AwayTeam.Score + match.HomeTeam.Score, item2: match.CreatedTicks), match);
            }

            return keyValuePairs.OrderByDescending(k => k.Key);
        }

    }
}
