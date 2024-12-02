using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Models
{
    public class Team : ITeam
    {
        private string _name;
        private int _score;

        public Team(string name)
        {
            Name = name;
            Score = 0;
        }

        public string Name { get; set; }
        public int Score { get; set; }
    }
}
