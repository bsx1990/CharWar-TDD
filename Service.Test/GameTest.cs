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
        [Retry(20)]
        [Category("GameTest")]
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
        [Category("GameTest")]
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
        [Category("GameTest")]
        public void NextCandidate_Should_NotBe3_When_GenerateNewCandidateWithMaxValueOfCheckerboardIs2()
        {
            var game = new Game();
            game.Place(0,0);
            game.Place(1,0);
            game.Place(0,0);
            
            Assert.AreNotEqual(3, game.NextCandidate.Value);
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

        [Test]
        [Repeat(10000)]
        [Category("CandidateTest")]
        public void CandidateValue_Should_NotLargerThanUpperLimit([Range(1,20)] int upperLimit)
        {
            var candidate = CandidateBuilder.Build(upperLimit);
            
            var maxValueOfCandidate = 9;
            Assert.LessOrEqual(candidate.Value, maxValueOfCandidate);
        }

        [Test]
        [TestCaseSource(typeof(MyTestCaseData), nameof(MyTestCaseData.CandidateBuildTestCases))]
        [Retry(100)]
        [Category("CandidateTest")]
        public void CandidateValue_Should_EqualToSpecifiedValue(int specifiedValue, int upperLimit)
        {
            var candidate = CandidateBuilder.Build(upperLimit);
            
            Assert.AreEqual(specifiedValue, candidate.Value);
        }
    }
}