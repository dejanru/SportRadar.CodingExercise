namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IMatch
    {
        /// <summary>
        /// Gets or sets the home team.
        /// </summary>
        /// <value>
        /// The home team.
        /// </value>
        ITeam HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the away team.
        /// </summary>
        /// <value>
        /// The away team.
        /// </value>
        ITeam AwayTeam { get; set; }

        /// <summary>
        /// Gets or sets the created date in ticks.
        /// Needed for correct ordering.
        /// </summary>
        /// <value>
        /// The created date in ticks.
        /// </value>
        long CreatedTicks { get; }
    }
}
