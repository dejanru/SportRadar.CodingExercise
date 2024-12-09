namespace SportRadar.CodingExercise.Lib.Interfaces
{
    /// <summary>
    /// Initializes world cup tour system.
    /// </summary>
    public interface IWorldCupService
    {
        /// <summary>
        /// Gets the running - ongoing matches.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<IMatch>> GetRunningMatches();

        /// <summary>
        /// Gets the archive matches. Means matches already finished.
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
        /// Updates the score. Only running matches affected.
        /// </summary>
        /// <param name="homeTeam">The home team name.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="homeScore">The home score.</param>
        /// <param name="awayScore">The away score.</param>
        /// <returns></returns>
        Task<ICollection<IMatch>> UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore);

        /// <summary>
        /// Finishes the match.
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <returns>Collections of running matches and finished matches.</returns>
        Task<Tuple<ICollection<IMatch>, ICollection<IMatch>>> FinishMatch(string homeTeam, string awayTeam);
    }
}
