using NSubstitute;
using NSubstitute.Core;
using SportRadar.CodingExercise.Lib.Interfaces;
using SportRadar.CodingExercise.Lib.Models;
using SportRadar.CodingExercise.Lib.Services;

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
        //private IWorldCupService _worldCupServiceInstance;
        private IMatch _match;
        private ITeam _team;
        private IWorldCupService _worldCupService;

        private WorldCupHandler handler;

        [SetUp]
        public void Setup()
        {
            _match = Substitute.For<IMatch>();
            _team = Substitute.For<ITeam>();
            _worldCupService = Substitute.For<IWorldCupService>();

            handler = new WorldCupHandler(_worldCupService, _match, _team);
        }

        [Test]
        public void TestWorldCup_Infrastructure()
        {
            Assert.Pass("Are tests running?");
        }

        [Test]
        public void TestWorldCup_WorldCupExists()
        {
            Assert.That(handler, Is.Not.Null);
        }

        [Test]
        public void TestWorldCup_WeShallHaveRunningMatches_And_ArchiveMatches()
        {
            Assert.That(handler.GetRunningMatches(), Is.Not.Null);
            Assert.That(handler.GetArchiveMatches(), Is.Not.Null);
        }

        [Test]
        [TestCase("Mexico", "Canada")]
        [TestCase("Spain", "Brazil")]
        [TestCase("Germany", "France")]
        [TestCase("Uruguay", "Italy")]
        [TestCase("Argentina", "Australia")]
        public void TestWorldCup_StartNewMatch(string homeTeam, string awayTeam)
        {
            // handler.GetRunningMatches();
            // check if already contains combination of home/away match
            // Arrange
            IMatch fixtureMatch = new Match(homeTeam, awayTeam);
            handler.StartNewMatch(homeTeam, awayTeam)
                    .ReturnsForAnyArgs(fixtureMatch);

            // well, use service to calculate the data and then compare with fixture and assert for correctness
            var calculatedData = handler.StartNewMatch(homeTeam, awayTeam);

            Assert.IsNotNull(calculatedData);
            Assert.That(calculatedData, Is.EqualTo(fixtureMatch));
        }

        [Test]
        [TestCase("Mexico", "Canada", 0, 1)]
        [TestCase("Mexico", "Canada", 0, 2)]
        [TestCase("Mexico", "Canada", 0, 3)]
        [TestCase("Mexico", "Canada", 0, 4)]
        [TestCase("Mexico", "Canada", 0, 5)]
        public void TestWorldCup_UpdateScore(string homeTeam, string awayTeam, int homeScore, int awayScore)
        {
            // Arrange
            IMatch fixtureMatch = new Match(homeTeam, awayTeam);
            handler.StartNewMatch(homeTeam, awayTeam)
                    .ReturnsForAnyArgs(fixtureMatch);

            handler.StartNewMatch(homeTeam, awayTeam);

            var runningMatches = handler.GetRunningMatches();
            Assert.IsNotNull(runningMatches);

            var match = runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);
            Assert.IsNotNull(match);

            handler.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            // ba careful : what if null?
            var updatedMatch = runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);

            Assert.IsNotNull(updatedMatch);
            Assert.That(updatedMatch.HomeTeam.Score, Is.EqualTo(homeScore));
            Assert.That(updatedMatch.AwayTeam.Score, Is.EqualTo(awayScore));
        }

        [Test]
        [TestCase("Mexico", "Canada")]
        public void TestWorldCup_FinishMatchInProgress(string homeTeam, string awayTeam)
        {
            // start new match
            // check list of started matches - shall be included
            // just update the score - to have some data there
            // finish match
            // check list of started matches - shall be removed
            // check list of archive matches - shall be included
        }

        [Test]
        public void TestWorldCup_GetSummaryOfMatches()
        {
            // NOTE : handles only matches in progress, finished shall not be contained

            // start those matches :
            //      a.Mexico 0 - Canada 5
            //      b.Spain 10 - Brazil 2
            //      c.Germany 2 - France 2
            //      d.Uruguay 6 - Italy 6
            //      e.Argentina 3 - Australia 1
            IMatch fixtureMatch = new Match("Mexico", "Canada");
            handler
                            .StartNewMatch("Mexico", "Canada")
                            .ReturnsForAnyArgs(fixtureMatch);

            handler.StartNewMatch("Mexico", "Canada");
            handler.StartNewMatch("Spain", "Brazil");
            handler.StartNewMatch("Germany", "France");
            handler.StartNewMatch("Uruguay", "Italy");
            handler.StartNewMatch("Argentina", "Australia");

            var runningMatches = handler.GetRunningMatches();
            // update scores to reflect provided scores (shall be one call since only time of match start is important for order)
            // we shall not call finish match - remember, we only want summary of matches in progress

            // get summary in correct order :
            //      1.Uruguay 6 - Italy 6
            //      2.Spain 10 - Brazil 2
            //      3.Mexico 0 - Canada 5
            //      4.Argentina 3 - Australia 1
            //      5.Germany 2 - France 2
        }
    }
}