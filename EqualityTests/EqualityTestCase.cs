using System;

namespace EqualityTests
{
    public class EqualityTestCase
    {
        public EqualityTestCase(object firstInstance, object secondInstance, bool expectedResult)
        {
            if (firstInstance == null)
            {
                throw new ArgumentNullException("firstInstance");
            }
            if (secondInstance == null)
            {
                throw new ArgumentNullException("secondInstance");
            }
            FirstInstance = firstInstance;
            SecondInstance = secondInstance;
            ExpectedResult = expectedResult;
        }

        public object FirstInstance { get; set; }
        public object SecondInstance { get; set; }
        public bool ExpectedResult { get; set; }
    }
}