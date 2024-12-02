using NSubstitute;
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
        private IWorldCupService _worldCupServiceInstance;
        private IMatch _match;
        private ITeam _team;

        private WorldCupServiceHandler handler;

        [SetUp]
        public void Setup()
        {
            _worldCupServiceInstance = Substitute.For<IWorldCupService>();
            _match = Substitute.For<IMatch>();
            _team = Substitute.For<ITeam>();
            handler = new WorldCupServiceHandler(_worldCupServiceInstance, _match, _team);
        }

        [Test]
        public void TestWorldCup_Infrastructure()
        {
            Assert.Pass("Are tests running?");
        }

        [Test]
        public void TestWorldCup_WorldCupExists()
        {
            Assert.IsNotNull(_worldCupServiceInstance);
        }

        [Test]
        public void TestWorldCup_WeShallHaveRunningMatches_And_ArchiveMatches()
        {
            Assert.IsNotNull(_worldCupServiceInstance.GetRunningMatches());
            Assert.IsNotNull(_worldCupServiceInstance.GetArchiveMatches());
        }

        [Test]
        [TestCase("Mexico", "Canada")]
        [TestCase("Spain", "Brazil")]
        [TestCase("Germany", "France")]
        [TestCase("Uruguay", "Italy")]
        [TestCase("Argentina", "Australia")]
        public void TestWorldCup_StartNewMatch(string homeTeam, string awayTeam)
        {
            _worldCupServiceInstance.GetRunningMatches();
            // check if already contains combination of home/away match
            // Arrange
            IMatch fixtureMatch = new Match(homeTeam, awayTeam);
            var result = _worldCupServiceInstance
                            .StartNewMatch(homeTeam, awayTeam)
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
            IMatch fixtureMatch = new Match(homeTeam, awayTeam);
            var result = _worldCupServiceInstance
                .StartNewMatch(homeTeam, awayTeam)
                .ReturnsForAnyArgs(fixtureMatch);

            handler.StartNewMatch(homeTeam, awayTeam);

            var runningMatches = handler.GetRunningMatches();
            Assert.IsNotNull(runningMatches);

            var match = runningMatches.FirstOrDefault(x => x.homeTeam.Name == homeTeam && x.awayTeam.Name == awayTeam);
            Assert.IsNotNull(match);

            handler.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);

            var updatedMatch = runningMatches.FirstOrDefault(x => x.homeTeam.Name == homeTeam && x.awayTeam.Name == awayTeam);

            Assert.IsNotNull(updatedMatch);
            Assert.That(updatedMatch.homeTeam.Score, Is.EqualTo(homeScore));
            Assert.That(updatedMatch.awayTeam.Score, Is.EqualTo(awayScore));
        }
    }
}