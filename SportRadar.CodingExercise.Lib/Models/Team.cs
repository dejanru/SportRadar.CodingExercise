using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Models
{
    public class Team : ITeam
    {
        private string _name;
        private int _score;

        /// <summary>
        /// Initializes a new instance of the <see cref="Team"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Team(string name)
        {
            Name = name;
            Score = 0;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }
    }
}
