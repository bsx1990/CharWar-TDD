using NUnit.Framework;
using Service.Utility;
using Service.Utility.Exceptions;

namespace Service.Test
{
    [Category("GameTest")]
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_ShowInitUI_When_StartANewGame()
        {
            var game = new Game();
            const string emptyCheckerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";

            Utility.CheckGameOutput(emptyCheckerboard, initCandidate, game.Print());
        }

        [Test]
        public void Should_BeUpdated_When_PlacePieceOnAnEmptyCheckerboard()
        {
            var game = new Game();
            game.Place(0, 0);
            
            const string checkerboard = "[ 1] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, game.Print());
        }
        
        [Test]
        public void Should_BeInvalid_When_PlacePieceOnAnotherPiece()
        {
            var game = new Game();
            game.Place(0, 0);
            var exception = Assert.Throws(typeof(PlaceException), ()=>game.Place(0, 0));
            Assert.That(exception.Message, Is.EqualTo("row:0 column:0 not empty"));
            
            const string checkerboard = "[ 1] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, game.Print());
        }

        [Test]
        public void Should_BeCombinedTo2AndScore1_When_Place1()
        {
            var game = new Game();
            game.Place(0, 0);
            game.Place(1, 1);
            
            const string checkerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [ 2] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, game.Print());
            Assert.AreEqual(1, game.Score);
        }

        [Test]
        [Retry(20)]
        public void NextCandidate_Should_HaveChancesToBe2_When_GenerateNewCandidateWithMaxValueOfCheckerboardIs2()
        {
            var game = new Game();
            game.Place(0,0);
            game.Place(1,0);
            game.Place(0,0);
            
            Assert.AreEqual(2, game.NextCandidate.Value);
        }

        [Test]
        [Retry(20)]
        public void NextCandidate_Should_HaveChancesToBe1_When_GenerateNewCandidateWithMaxValueOfCheckerboardIs2()
        {
            var game = new Game();
            game.Place(0,0);
            game.Place(1,0);
            game.Place(0,0);
            
            Assert.AreEqual(1, game.NextCandidate.Value);
        }

        [Test]
        [Repeat(100)]
        public void NextCandidate_Should_NotBe3_When_GenerateNewCandidateWithMaxValueOfCheckerboardIs2()
        {
            var game = new Game();
            game.Place(0,0);
            game.Place(1,0);
            game.Place(0,0);
            
            Assert.AreNotEqual(3, game.NextCandidate.Value);
        }
    }
}