using NUnit.Framework;
using Service.Utility;

namespace Service.Test
{
    [Category("CombineTest")]
    public class CombineTest
    {
        [Test]
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