using NUnit.Framework;

namespace Service.Test
{
    internal static class Utility
    {
        private static string ExpectedOutPut(string checkerboard, string candidate)
        {
            return $@"{checkerboard}
{candidate}";
        }

        public static void CheckGameOutput(string emptyCheckerboard, string initCandidate, string gameOutput)
        {
            Assert.AreEqual(ExpectedOutPut(emptyCheckerboard, initCandidate), gameOutput);
        }
    }
}