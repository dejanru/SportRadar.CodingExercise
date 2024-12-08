using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Models
{
    public class Match : IMatch
    {
        private ITeam _homeTeam;
        private ITeam _awayTeam;
        private long _created_ticks;

        /// <summary>
        /// Initializes a new instance of the <see cref="Match"/> class.
        /// </summary>
        /// <param name="homeTeamName">Name of the home team.</param>
        /// <param name="awayTeamName">Name of the away team.</param>
        /// <exception cref="System.ArgumentException">Home team is same as away team and this is not allowed.</exception>
        public Match(string homeTeamName, string awayTeamName)
        {
            if (homeTeamName == awayTeamName)
            {
                throw new ArgumentException("Home team is same as away team and this is not allowed.");
            }

            _homeTeam = new Team(homeTeamName);
            _awayTeam = new Team(awayTeamName);
            _created_ticks = DateTime.UtcNow.Ticks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Match"/> class.
        /// </summary>
        /// <param name="homeTeamName">Name of the home team.</param>
        /// <param name="awayTeamName">Name of the away team.</param>
        /// <param name="homeTeamScore">The home team score.</param>
        /// <param name="awayTeamScore">The away team score.</param>
        /// <exception cref="System.ArgumentException">Home team is same as away team and this is not allowed.</exception>
        public Match(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore)
        {
            if (homeTeamName == awayTeamName)
            {
                throw new ArgumentException("Home team is same as away team and this is not allowed.");
            }

            _homeTeam = new Team(homeTeamName);
            _homeTeam.Score = homeTeamScore;
            _awayTeam = new Team(awayTeamName);
            _awayTeam.Score = awayTeamScore;
            _created_ticks = DateTime.UtcNow.Ticks;
        }

        /// <summary>
        /// Gets or sets the home team.
        /// </summary>
        /// <value>
        /// The home team.
        /// </value>
        public ITeam HomeTeam { get => _homeTeam; set => _homeTeam = value; }
        /// <summary>
        /// Gets or sets the away team.
        /// </summary>
        /// <value>
        /// The away team.
        /// </value>
        public ITeam AwayTeam { get => _awayTeam; set => _awayTeam = value; }
        public long CreatedTicks { get => _created_ticks;}
    }
}
