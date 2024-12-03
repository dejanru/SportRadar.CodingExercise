namespace SportRadar.CodingExercise.Lib.Interfaces
{
    public interface IMatch
    {
        ITeam HomeTeam { get; set; }
        ITeam AwayTeam { get; set; }
    }
}
