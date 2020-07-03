using NUnit.Framework;
using Service.Utility.Exceptions;

namespace Service.Test
{
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
    }
}