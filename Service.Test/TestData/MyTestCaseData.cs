using System.Collections;
using NUnit.Framework;

namespace Service.Test.TestData
{
    public class MyTestCaseData
    {
        public static IEnumerable CandidateBuildTestCases
        {
            get
            {
                const int specifiedUpperLimit = 9;
                const int maxValueOfCheckerboardUpperLimit = 20;
                for (var specifiedValue = 1; specifiedValue <= specifiedUpperLimit; specifiedValue++)
                {
                    for (var upperLimit = specifiedValue; upperLimit <= maxValueOfCheckerboardUpperLimit; upperLimit++)
                    {
                        yield return new TestCaseData(specifiedValue, upperLimit);
                    }
                }
            }
        }
    }
}