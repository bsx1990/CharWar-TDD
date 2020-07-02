using NUnit.Framework;

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
            const string emptyCheckerboard = @"[  ] [  ] [  ] [  ]
[  ] [  ] [  ] [  ]
[  ] [  ] [  ] [  ]
[  ] [  ] [  ] [  ]";
            const string initCandidate = "Next: 1; Current: 1";

            Utility.CheckGameOutput(emptyCheckerboard, initCandidate, game.ToString());
        }
    }
}