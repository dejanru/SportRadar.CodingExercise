using System.ComponentModel.DataAnnotations;

namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IMatch
    {
        ITeam HomeTeam { get; set; }
        ITeam AwayTeam { get; set; }
        long CreatedTicks { get; set; }
    }
}
