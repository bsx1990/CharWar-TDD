using NUnit.Framework;
using Service.Test.TestData;

namespace Service.Test
{
    [Category("CandidateTest")]
    public class CandidateBuilderTest
    {
        [Test]
        [Repeat(10000)]
        public void CandidateValue_Should_NotLargerThanUpperLimit([Range(1,20)] int upperLimit)
        {
            var candidate = CandidateBuilder.Build(upperLimit);
            
            var maxValueOfCandidate = 9;
            Assert.LessOrEqual(candidate.Value, maxValueOfCandidate);
        }

        [Test]
        [TestCaseSource(typeof(MyTestCaseData), nameof(MyTestCaseData.CandidateBuildTestCases))]
        [Retry(100)]
        public void CandidateValue_Should_EqualToSpecifiedValue(int specifiedValue, int upperLimit)
        {
            var candidate = CandidateBuilder.Build(upperLimit);
            
            Assert.AreEqual(specifiedValue, candidate.Value);
        }
    }
}