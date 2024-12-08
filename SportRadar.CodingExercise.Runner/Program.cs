using SportRadar.CodingExercise.Lib.Interfaces;
using SportRadar.CodingExercise.Lib.Services;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        IMatch match = null;
        ITeam team = null;
        IWorldCupService worldCupService = new WorldCupService();
        IWorldCupHandler handler = new WorldCupHandler(worldCupService, match, team);

        while (true)
        {
            Console.WriteLine("Enter a number between 1 and 5: ");
            Console.WriteLine("1 : Get current state (running matches and archive matches)");
            Console.WriteLine("2 : Start new match");
            Console.WriteLine("3 : Update score for existing match");
            Console.WriteLine("4 : Finish existing match");
            Console.WriteLine("5 : Get summary of matches");
            Console.WriteLine("6 : Fill test data for sample summary");
            Console.WriteLine("0 : Finish() and good bye.");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number) && number >= 0 && number <= 6)
            {
                try
                {
                    if (number == 0)
                    {
                        Console.WriteLine("Exiting program...");
                        break;
                    }
                    else if (number == 1)
                    {
                        // Get current state (running matches and archive matches)
                        GetCurrentState(handler);
                    }
                    else if (number == 2)
                    {
                        // Start new match"
                        Console.Write("enter Home team : ");
                        string homeTeam = Console.ReadLine();
                        Console.Write("enter Away team : ");
                        string awayTeam = Console.ReadLine();
                        handler.StartNewMatch(homeTeam, awayTeam);

                    }
                    else if (number == 3)
                    {
                        // Update score for existing match
                        Console.Write("select existing match:");

                        var runningMatches = handler.GetRunningMatches();
                        Console.WriteLine(@"---------------------------------------------------------");
                        Console.WriteLine(@"Running matches : " + runningMatches.Count);
                        int cnt = 0;
                        foreach (var item in runningMatches)
                        {

                            Console.WriteLine($"{cnt} : HomeTeam : {item.HomeTeam.Name}({item.HomeTeam.Score}) | AwayTeam : {item.AwayTeam.Name}({item.AwayTeam.Score})");
                            cnt++;
                        }
                        string matchId = Console.ReadLine();
                        int.TryParse(matchId, out int matchNumber);

                        var mt = runningMatches.ElementAt(matchNumber);
                        Console.Write($"Provide score for {mt.HomeTeam.Name} :");
                        int.TryParse(Console.ReadLine(), out int scoreHome);
                        Console.Write($"Provide score for {mt.AwayTeam.Name} :");
                        int.TryParse(Console.ReadLine(), out int scoreAway);

                        handler.UpdateScore(mt.HomeTeam.Name, mt.AwayTeam.Name, scoreHome, scoreAway);

                    }
                    else if (number == 4)
                    {
                        // Finish existing match
                        Console.Write("select existing match:");

                        var runningMatches = handler.GetRunningMatches();
                        Console.WriteLine(@"---------------------------------------------------------");
                        Console.WriteLine(@"Running matches : " + runningMatches.Count);
                        int cnt = 0;
                        foreach (var item in runningMatches)
                        {

                            Console.WriteLine($"{cnt} : HomeTeam : {item.HomeTeam.Name}({item.HomeTeam.Score}) | AwayTeam : {item.AwayTeam.Name}({item.AwayTeam.Score})");
                            cnt++;
                        }
                        string matchId = Console.ReadLine();
                        int.TryParse(matchId, out int matchNumber);
                        var mt = runningMatches.ElementAt(matchNumber);
                        handler.FinishMatch(mt.HomeTeam.Name, mt.AwayTeam.Name);
                    }
                    else if (number == 5)
                    {
                        // Get summary of matches
                        Console.WriteLine($"You entered: {number}");

                        var summary = handler.GetSummaryOfMatches();
                        DisplaySummary(summary);
                    }
                    else if (number == 6)
                    {
                        // Fill test data for sample summary
                        Console.WriteLine($"You entered: {number}");

                        handler.StartNewMatch("Mexico", "Canada");
                        handler.UpdateScore("Mexico", "Canada", 0, 5);

                        handler.StartNewMatch("Spain", "Brazil");
                        handler.UpdateScore("Spain", "Brazil", 10, 2);

                        handler.StartNewMatch("Germany", "France");
                        handler.UpdateScore("Germany", "France", 2, 2);

                        handler.StartNewMatch("Uruguay", "Italy");
                        handler.UpdateScore("Uruguay", "Italy", 6, 6);

                        handler.StartNewMatch("Argentina", "Australia");
                        handler.UpdateScore("Argentina", "Australia", 3, 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
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