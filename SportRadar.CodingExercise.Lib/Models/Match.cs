using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Models
{
    public class Match : IMatch
    {
        private ITeam _homeTeam;
        private ITeam _awayTeam;
        private long _created_ticks;

        public Match(string homeTeamName, string awayTeamName)
        {
            _homeTeam = new Team(homeTeamName);
            _awayTeam = new Team(awayTeamName);
            _created_ticks = DateTime.UtcNow.Ticks;
        }

        public Match(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore)
        {
            _homeTeam = new Team(homeTeamName);
            _homeTeam.Score = homeTeamScore;
            _awayTeam = new Team(awayTeamName);
            _awayTeam.Score = awayTeamScore;
            _created_ticks = DateTime.UtcNow.Ticks;

        }

        public ITeam HomeTeam { get => _homeTeam; set => _homeTeam = value; }
        public ITeam AwayTeam { get => _awayTeam; set => _awayTeam = value; }
        public long CreatedTicks { get => _created_ticks; set => throw new NotImplementedException(); }
    }
}
