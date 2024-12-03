using SportRadar.CodingExercise.Lib.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace SportRadar.CodingExercise.Lib.Models
{
    public class Match : IMatch
    {
        private ITeam _homeTeam;
        private ITeam _awayTeam;

        public Match(string homeTeamName, string awayTeamName)
        {
            _homeTeam = new Team(homeTeamName);
            _awayTeam = new Team(awayTeamName);
        }

        public ITeam HomeTeam { get => _homeTeam; set => _homeTeam = value; }
        public ITeam AwayTeam { get => _awayTeam; set => _awayTeam = value; }
    }
}
