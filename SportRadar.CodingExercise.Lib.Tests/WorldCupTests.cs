using NSubstitute;
using SportRadar.CodingExercise.Lib.Interfaces;

namespace SportRadar.CodingExercise.Lib.Tests
{
    /*
    We need to have a set of matches.
    => on startup prepare in-memory "database" to be able to store it. Simple (sortoff) collection shall be enough.
    - use Scoreboard (collection) for currently played matches
    - use SummaryMatches (collection) for all played matches that are already finished.
    - implement StartNewMatch
        - check for matches currently played - the new one shall not be already played
        - check for matches already played - the new one shall not be already played
    - UpdateScore for currently played match (2 params, homeTeam/awayTeam)
    - FinishMatch. Removes match from the scoreboard and adds it to summaryCollection.
    - GetSummaryOfMatches (from currently played). NOTE : already finished shall not be contained (prepare another call for this)
     */


    public class WorldCupTests
    {
        private IWorldCup _worldCupInstance;


        [SetUp]
        public void Setup()
        {
            _worldCupInstance = Substitute.For<IWorldCup>();

        }

        [Test]
        public void TestWorldCup_Infrastructure()
        {
            Assert.Pass("Are tests running?");
        }

        [Test]
        public void TestWorldCup_WorldCupExists()
        {
            Assert.IsNotNull(_worldCupInstance);
        }

        [Test]
        public void TestWorldCup_WeShallHaveRunningMatches_And_ArchiveMatches()
        {
            Assert.IsNotNull(_worldCupInstance.GetRunningMatches());
            Assert.IsNotNull(_worldCupInstance.GetArchiveMatches());
        }

        [Test]
        [TestCase("Mexico", "Canada")]
        [TestCase("Spain", "Brazil")]
        [TestCase("Germany", "France")]
        [TestCase("Uruguay", "Italy")]
        [TestCase("Argentina", "Australia")]
        public void TestWorldCup_StartNewMatch(string homeTeam, string awayTeam)
        {
            _worldCupInstance.GetRunningMatches();
            // check if already contains combination of home/away match
            var match = _worldCupInstance.StartNewMatch(homeTeam, awayTeam);
            Assert.IsNotNull(match);
        }

        [Test]
        [TestCase("Mexico", "Canada", 0, 1)]
        [TestCase("Mexico", "Canada", 0, 2)]
        [TestCase("Mexico", "Canada", 0, 3)]
        [TestCase("Mexico", "Canada", 0, 4)]
        [TestCase("Mexico", "Canada", 0, 5)]
        public void TestWorldCup_UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            var runningMatches = _worldCupInstance.GetRunningMatches();
            Assert.IsNotNull(runningMatches);
            
            var matchStarted = _worldCupInstance.StartNewMatch(homeTeam, awayTeam);


            var match = runningMatches.FirstOrDefault(x => x.homeTeam.Name == homeTeam && x.awayTeam.Name == awayTeam);
            Assert.IsNotNull(match);
        }
    }
}