using NUnit.Framework;
using Service.Utility;
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
        [Category("GameTest")]
        public void Should_ShowInitUI_When_StartANewGame()
        {
            var game = new Game();
            const string emptyCheckerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";

            Utility.CheckGameOutput(emptyCheckerboard, initCandidate, game.Print());
        }

        [Test]
        [Category("GameTest")]
        public void Should_BeUpdated_When_PlacePieceOnAnEmptyCheckerboard()
        {
            var game = new Game();
            game.Place(0, 0);
            
            const string checkerboard = "[ 1] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, game.Print());
        }
        
        [Test]
        [Category("GameTest")]
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
        [Category("GameTest")]
        public void Should_BeCombinedTo2_When_Place1()
        {
            var game = new Game();
            game.Place(0, 0);
            game.Place(1, 1);
            
            const string checkerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [ 2] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";
            Utility.CheckGameOutput(checkerboard, initCandidate, game.Print());
        }
               
        [Test]
        [Category("CombineTest")]
        public void CommonCombineTest_Should_BeCombinedTo2_When_Place1()
        {
            var checkerboard = new[,]
            {
                {new Piece(1), new Piece(), new Piece(), new Piece()},
                {new Piece(), new Piece(1), new Piece(), new Piece()},
                {new Piece(), new Piece(), new Piece(), new Piece()},
                {new Piece(), new Piece(), new Piece(), new Piece()}
            };
            var game = new Game();
            game.Combine(checkerboard, 1, 1);
            
            const string expectCheckerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [ 2] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            Assert.AreEqual(expectCheckerboard, checkerboard.Print());
        } 
        
        [Test]
        [Category("CombineTest")]
        public void CornerCombineTest_Should_BeCombinedTo2_When_Place1()
        {
            var checkerboard = new[,]
            {
                {new Piece(1), new Piece(), new Piece(), new Piece()},
                {new Piece(), new Piece(1), new Piece(), new Piece()},
                {new Piece(), new Piece(), new Piece(), new Piece()},
                {new Piece(), new Piece(), new Piece(), new Piece()}
            };
            var game = new Game();
            game.Combine(checkerboard, 0, 0);
            
            const string expectCheckerboard = "[ 2] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]\r\n[  ] [  ] [  ] [  ]";
            Assert.AreEqual(expectCheckerboard, checkerboard.Print());
        }
        
        [Test]
        [Category("CombineTest")]
        public void MultiCombineTest_Should_BeCombinedTo2_When_Place1()
        {
            var checkerboard = new[,]
            {
                {new Piece(1), new Piece(2), new Piece(3), new Piece()},
                {new Piece(), new Piece(1), new Piece(), new Piece()},
                {new Piece(1), new Piece(), new Piece(5), new Piece()},
                {new Piece(), new Piece(), new Piece(), new Piece()}
            };
            var game = new Game();
            game.Combine(checkerboard, 1, 1);
            
            const string expectCheckerboard = "[  ] [  ] [  ] [  ]\r\n[  ] [ 4] [  ] [  ]\r\n[  ] [  ] [ 5] [  ]\r\n[  ] [  ] [  ] [  ]";
            Assert.AreEqual(expectCheckerboard, checkerboard.Print());
        }
    }
}