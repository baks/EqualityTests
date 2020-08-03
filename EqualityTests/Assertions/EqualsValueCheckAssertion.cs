using System;
using EqualityTests.Exception;
using AutoFixture.Idioms;

namespace EqualityTests.Assertions
{
    public class EqualsValueCheckAssertion : IdiomaticAssertion
    {
        private readonly IEqualityTestCaseProvider equalityTestCaseProvider;

        public EqualsValueCheckAssertion(IEqualityTestCaseProvider equalityTestCaseProvider)
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
                var result = testCase.FirstInstance.Equals(testCase.SecondInstance);

                if (result != testCase.ExpectedResult)
                {
                    if (testCase.ExpectedResult)
                    {
                        throw new EqualsValueCheckException(
                            string.Format(
                                "Expected type {0} to perform value check but looks like it performs identity check",
                                type.Name));
                    }

                    throw new EqualsValueCheckException(string.Format("Expected {0} to be not equal to {1}",
                        testCase.FirstInstance, testCase.SecondInstance));
                }
            }
        }
    }
}
