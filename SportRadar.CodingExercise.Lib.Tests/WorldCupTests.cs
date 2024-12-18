using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SportRadar.CodingExercise.Lib.Interfaces;
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
            // check if already contains combination of home/away match
            // Arrange
            IMatch fixtureMatch = new Models.Match(homeTeam, awayTeam);
            ICollection<IMatch> matches = [fixtureMatch];
            handler.StartNewMatch(homeTeam, awayTeam).ReturnsForAnyArgs(matches);

            // well, use service to calculate the data and then compare with fixture and assert for correctness
            var calculatedData = handler.StartNewMatch(homeTeam, awayTeam);

            Assert.IsNotNull(calculatedData);
            Assert.That(calculatedData, Is.EqualTo(matches));
        }

        [Test]
        [TestCase("Mexico", "Canada")]
        public void TestWorldCup_StartNewMatch_AlreadyExists(string homeTeam, string awayTeam)
        {
            // matches in progress
            ICollection<IMatch> matches = new List<IMatch>();
            matches.Add(new Models.Match("Mexico", "Canada", 0, 5));
            matches.Add(new Models.Match("Spain", "Brazil", 10, 2));
            matches.Add(new Models.Match("Germany", "France", 2, 2));
            matches.Add(new Models.Match("Uruguay", "Italy", 6, 6));
            matches.Add(new Models.Match("Argentina", "Australia", 3, 1));

            _worldCupService.GetRunningMatches().Returns(matches);
            ICollection<IMatch> matches_with_added = new List<IMatch>(matches);
            matches_with_added.Add(new Models.Match("Mexico", "Canada"));
            Exception exc = new Exception("Match already in progress");

            _worldCupService.StartNewMatch(homeTeam, awayTeam).Throws(exc);

            var runningMatches = handler.GetRunningMatches();
            Assert.IsNotNull(runningMatches);

            try
            {
                var addedOneMatch = handler.StartNewMatch(homeTeam, awayTeam).Throws(new Exception());
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.SameAs(exc.Message));
            }
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
            IMatch fixtureMatch = new Models.Match(homeTeam, awayTeam);
            ICollection<IMatch> matches =
            [
                new Models.Match("Mexico", "Canada"),
                new Models.Match("Spain", "Brazil"),
                new Models.Match("Germany", "France"),
                new Models.Match("Uruguay", "Italy"),
                new Models.Match("Argentina", "Australia"),
            ];
            _worldCupService.StartNewMatch(homeTeam, awayTeam).Returns(matches);
            _worldCupService.GetRunningMatches().Returns(matches);

            fixtureMatch.HomeTeam.Score = homeScore;
            fixtureMatch.AwayTeam.Score = awayScore;
            _worldCupService.UpdateScore(homeTeam, awayTeam, homeScore, awayScore).Returns(matches);

            var runningMatches = handler.GetRunningMatches();
            Assert.IsNotNull(runningMatches);

            var match = runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);
            Assert.IsNotNull(match);

            handler.UpdateScore(homeTeam, awayTeam, homeScore, awayScore);
            // update list what shall be returned after update
            matches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam).HomeTeam.Score = homeScore;
            matches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam).AwayTeam.Score = awayScore;

            // be careful : what if null?
            var updatedMatch = runningMatches.FirstOrDefault(x => x.HomeTeam.Name == homeTeam && x.AwayTeam.Name == awayTeam);

            Assert.IsNotNull(updatedMatch);
            Assert.That(updatedMatch.HomeTeam.Score, Is.EqualTo(homeScore));
            Assert.That(updatedMatch.AwayTeam.Score, Is.EqualTo(awayScore));
        }

        [Test]
        [TestCase("Mexico", "Canada")]
        [TestCase("Spain", "Brazil")]
        [TestCase("Germany", "France")]
        [TestCase("Uruguay", "Italy")]
        [TestCase("Argentina", "Australia")]
        public void TestWorldCup_FinishMatchInProgress(string homeTeam, string awayTeam)
        {
            // start new match
            IMatch fixtureMatch = new Models.Match(homeTeam, awayTeam);
            ICollection<IMatch> runningMatches = [fixtureMatch];
            ICollection<IMatch> archiveMatches = [];


            _worldCupService.StartNewMatch(homeTeam, awayTeam).Returns(runningMatches);

            fixtureMatch.HomeTeam.Score = 1;
            fixtureMatch.AwayTeam.Score = 0;
            _worldCupService.UpdateScore(homeTeam, awayTeam, 1, 0).Returns(runningMatches);

            _worldCupService.GetRunningMatches().Returns(runningMatches);
            _worldCupService.GetArchiveMatches().Returns(archiveMatches);


            // check list of started matches - shall be included
            // finish match
            // simulate moving between lists
            runningMatches = [];
            archiveMatches = [fixtureMatch];
            _worldCupService.FinishMatch(homeTeam, awayTeam).Returns(Tuple.Create(runningMatches, archiveMatches));

            // ACT
            var running_matches = handler.StartNewMatch(homeTeam, awayTeam);
            // just update the score - to have some data there
            running_matches = handler.UpdateScore(homeTeam, awayTeam, 1, 0);
            var matches_list = handler.FinishMatch(homeTeam, awayTeam);


            // check list of started matches - shall be removed
            // check list of archive matches - shall be included
            Assert.That(homeTeam, Is.Not.EqualTo(awayTeam));
            Assert.That(matches_list.Item1, Is.Empty);
            Assert.That(matches_list.Item2, Is.EqualTo(running_matches));
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

            // could be simpler, but we need to have some time betweeen ticks to be able to distinguish correct order of starting match
            ICollection<IMatch> matches = new List<IMatch>();
            matches.Add(new Models.Match("Mexico", "Canada", 0, 5));
            matches.Add(new Models.Match("Spain", "Brazil", 10, 2));
            matches.Add(new Models.Match("Germany", "France", 2, 2));
            matches.Add(new Models.Match("Uruguay", "Italy", 6, 6));
            matches.Add(new Models.Match("Argentina", "Australia", 3, 1));

            _worldCupService.GetRunningMatches().Returns(matches);

            // check we have matches data
            var runningMatches = handler.GetRunningMatches();
            Assert.IsNotNull(runningMatches);

            var res = handler.GetSummaryOfMatches();
            // update scores to reflect provided scores (shall be one call since only time of match start is important for order)
            // mazbe we do not need it in this test
            // we shall not call finish match - remember, we only want summary of matches in progress

            // get summary in correct order :
            //      1.Uruguay 6 - Italy 6
            //      2.Spain 10 - Brazil 2
            //      3.Mexico 0 - Canada 5
            //      4.Argentina 3 - Australia 1
            //      5.Germany 2 - France 2
            var resultsList = res.ToList();
            Assert.That(resultsList[0].Value.HomeTeam.Name, Is.EqualTo("Uruguay"));
            Assert.That(resultsList[1].Value.HomeTeam.Name, Is.EqualTo("Spain"));
            Assert.That(resultsList[2].Value.HomeTeam.Name, Is.EqualTo("Mexico"));
            Assert.That(resultsList[3].Value.HomeTeam.Name, Is.EqualTo("Argentina"));
            Assert.That(resultsList[4].Value.HomeTeam.Name, Is.EqualTo("Germany"));
        }
    }
}