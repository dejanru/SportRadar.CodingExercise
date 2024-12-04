using SportRadar.CodingExercise.Lib.Interfaces;
using SportRadar.CodingExercise.Lib.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        IMatch match = null;
        ITeam team = null;
        IWorldCupService worldCupService = new WorldCupService();
        try
        {
            IWorldCupHandler handler = new WorldCupHandler(worldCupService, match, team);
            handler.StartNewMatch("Slo", "Cro");

            handler.StartNewMatch("England", "Wales");
            GetCurrentState(handler);

            handler.UpdateScore("Slo", "Cro", 2, 0);
            handler.StartNewMatch("USA", "Mexico");

            GetCurrentState(handler);

            try
            {
                handler.UpdateScore("Slo", "USA", 2, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            handler.StartNewMatch("Japan", "Korea");

            GetCurrentState(handler);

            handler.UpdateScore("Slo", "Cro", 2, 1);

            handler.UpdateScore("England", "Wales", 3, 3);
            handler.UpdateScore("USA", "Mexico", 4, 2);
            handler.UpdateScore("Japan", "Korea", 3, 0);

            var summary = handler.GetSummaryOfMatches();
            DisplaySummary(summary);

            handler.FinishMatch("Slo", "Cro");

            GetCurrentState(handler);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void GetCurrentState(IWorldCupHandler handler)
    {
        var runningMatches = handler.GetRunningMatches();
        Console.WriteLine(@"---------------------------------------------------------");
        Console.WriteLine(@"Running matches : " + runningMatches.Count);
        foreach (var item in runningMatches)
        {

            Console.WriteLine(@"HomeTeam : " + item.HomeTeam.Name + "[" + item.HomeTeam.Score + "]");
            Console.WriteLine(@"AwayTeam : " + item.AwayTeam.Name + "[" + item.AwayTeam.Score + "]");
            Console.WriteLine(@"----");
        }
        var archiveMatches = handler.GetArchiveMatches();
        Console.WriteLine(@"Archive matches : " + archiveMatches.Count);
        foreach (var item in archiveMatches)
        {

            Console.WriteLine(@"HomeTeam : " + item.HomeTeam.Name + "[" + item.HomeTeam.Score + "]");
            Console.WriteLine(@"AwayTeam : " + item.AwayTeam.Name + "[" + item.AwayTeam.Score + "]");
        }
        Console.WriteLine(@"---------------------------------------------------------");
    }

    private static void DisplaySummary(IOrderedEnumerable<KeyValuePair<Tuple<int, long>, IMatch>> summary)
    {
        Console.WriteLine(@"---------------------------------------------------------");
        Console.WriteLine(@"Running matches summary: " + summary.Count());
        foreach (var item in summary)
        {

            Console.WriteLine(@"HomeTeam : " + item.Value.HomeTeam.Name + "[" + item.Value.HomeTeam.Score + "]");
            Console.WriteLine(@"AwayTeam : " + item.Value.AwayTeam.Name + "[" + item.Value.AwayTeam.Score + "]");
            Console.WriteLine(@"----");
        }
    }
}