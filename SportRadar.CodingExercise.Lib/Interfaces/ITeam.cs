namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface ITeam
    {
        /// <summary>
        /// Gets or sets the team name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the score for the team.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        int Score {  get; set; }
    }
}
