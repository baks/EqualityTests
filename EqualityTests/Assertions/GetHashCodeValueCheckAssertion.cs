using System;
using EqualityTests.Exception;
using AutoFixture.Idioms;

namespace EqualityTests.Assertions
{
    public class GetHashCodeValueCheckAssertion : IdiomaticAssertion
    {
        private readonly IEqualityTestCaseProvider equalityTestCaseProvider;

        public GetHashCodeValueCheckAssertion(IEqualityTestCaseProvider equalityTestCaseProvider)
        {
            if (equalityTestCaseProvider == null)
            {
                throw new ArgumentNullException("equalityTestCaseProvider");
            }
            this.equalityTestCaseProvider = equalityTestCaseProvider;
        }

        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            foreach (var testCase in equalityTestCaseProvider.For(type))
            {
                var firstInstanceHashCode = testCase.FirstInstance.GetHashCode();
                var secondInstanceHashCode = testCase.SecondInstance.GetHashCode();
                var result = firstInstanceHashCode == secondInstanceHashCode;

                if (result != testCase.ExpectedResult)
                {
                    if (testCase.ExpectedResult)
                    {
                        throw new GetHashCodeValueCheckException(
                            string.Format(
                                "Expected type {0} GetHashCode method to compute bash based on value semantic not identity",
                                type.Name));
                    }

                    throw new GetHashCodeValueCheckException(
                        string.Format("Expected type {0} GetHashCode to return {1} hash codes for {2} and {3}",
                            type.Name, testCase.ExpectedResult ? "equal" : "not equal", testCase.FirstInstance,
                            testCase.SecondInstance));
                }
            }
        }
    }
}
