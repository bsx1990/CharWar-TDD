using NUnit.Framework;

namespace Service.Test
{
    internal static class Utility
    {
        public static void CheckGameOutput(string emptyCheckerboard, string initCandidate, int score, string gameOutput)
        {
            Assert.AreEqual($"Score:{score}\r\n{emptyCheckerboard}\r\n{initCandidate}", gameOutput);
        }
    }
}