using NUnit.Framework;
using Service.Utility;
using Service.Utility.Exceptions;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

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
            const int score = 0;
            const string emptyCheckerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";

            Utility.CheckGameOutput(emptyCheckerboard, initCandidate, score, game.Print());
        }

        [Test]
        public void Should_BeUpdated_When_PlacePieceOnAnEmptyCheckerboard()
        {
            var game = new Game();
            game.Place(0, 0);

            const int score = 0;
            const string checkerboard = "[ 1] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, score, game.Print());
        }
        
        [Test]
        public void Should_BeInvalid_When_PlacePieceOnAnotherPiece()
        {
            var game = new Game();
            game.Place(0, 0);
            var exception = Assert.Throws(typeof(PlaceException), ()=>game.Place(0, 0));
            Assert.That(exception.Message, Is.EqualTo("row:0 column:0 not empty"));

            const int score = 0;
            const string checkerboard = "[ 1] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, score, game.Print());
        }

        [Test]
        public void Should_BeCombinedTo2AndScore1_When_Place1()
        {
            var game = new Game();
            game.Place(0, 0);
            game.Place(1, 1);

            const int score = 1;
            const string checkerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [ 2] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, score, game.Print());
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

        [Test]
        public void Game_Should_BeOver_When_NoEmptyPositionAfterPlaceAPiece()
        {
            var checkerboardProperty = new [,]
            {
                {new Piece(1), new Piece(2), new Piece(3), new Piece(4)},
                {new Piece(5), new Piece(6), new Piece(7), new Piece(8)},
                {new Piece(9), new Piece(10), new Piece(11), new Piece(12)},
                {new Piece(13), new Piece(14), new Piece(15), new Piece()}
            };

            var game = Mock.Create<Game>();
            Mock.Arrange(() => game.Checkerboard).Returns(checkerboardProperty);
            Mock.Arrange(() => game.CurrentCandidate).Returns(new Piece(1));
            Mock.Arrange(() => game.NextCandidate).Returns(new Piece(1));
            game.Place(3,3);
            
            Assert.True(game.IsGameOver());
        }
    }
}