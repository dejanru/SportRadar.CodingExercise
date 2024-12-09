namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IWorldCupHandler
    {
        /// <summary>
        /// Gets the running matches.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<IMatch>> GetRunningMatches();

        /// <summary>
        /// Gets the archive matches.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<IMatch>> GetArchiveMatches();

        /// <summary>
        /// Starts the new match.
        /// </summary>
        /// <param name="homeTeam">The home team name.</param>
        /// <param name="awayTeam">The away team name.</param>
        /// <returns></returns>
        Task<ICollection<IMatch>> StartNewMatch(string homeTeam, string awayTeam);

        /// <summary>
        /// Updates the score.
        /// </summary>
        /// <param name="homeTeam">The home team name.</param>
        /// <param name="awayTeam">The away team name.</param>
        /// <param name="homeScore">The home score.</param>
        /// <param name="awayScore">The away score.</param>
        /// <returns></returns>
        Task<ICollection<IMatch>> UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);

        /// <summary>
        /// Finishes the match.
        /// </summary>
        /// <param name="homeTeam">The home team name.</param>
        /// <param name="awayTeam">The away team name.</param>
        /// <returns></returns>
        Task<Tuple<ICollection<IMatch>, ICollection<IMatch>>> FinishMatch(string homeTeam, string awayTeam);

        /// <summary>
        /// Gets the summary of matches.
        /// Get a summary of matches in progress ordered by their total score.
        /// The matches with the same total score will be returned ordered by the most recently started match in thescoreboard.
        /// </summary>
        /// <returns>ordered collection of matches</returns>
        Task<IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>>> GetSummaryOfMatches();
    }
}
