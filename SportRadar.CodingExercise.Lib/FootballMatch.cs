using System.ComponentModel.DataAnnotations;

namespace SportRadar.CodingExercise.Lib
{
    public class FootballMatch
    {
        private string _home_team;
        private string _away_team;
        private string _home_team_score;
        private string _away_team_score;

        public FootballMatch() { }


        [StringLength(50)]
        public string HomeTeam { get; set; }


    }
}
