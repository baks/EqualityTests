using System;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;

namespace EqualityTests.Assertions
{
    public class EqualityOperatorValueCheckAssertion : IdiomaticAssertion
    {
        private readonly IEqualityTestCaseProvider equalityTestCaseProvider;

        public EqualityOperatorValueCheckAssertion(IEqualityTestCaseProvider equalityTestCaseProvider)
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

            var equalityOperator = type.GetEqualityOperatorMethod();

            foreach (var testCase in equalityTestCaseProvider.For(type))
            {
                var result =
                    (bool) equalityOperator.Invoke(null, new[] {testCase.FirstInstance, testCase.SecondInstance});

                if (result != testCase.ExpectedResult)
                {
                    if (testCase.ExpectedResult)
                    {
                        throw new EqualityOperatorValueCheckException(
                            string.Format(
                                "Expected type {0} == operator to perform value check but looks like it performs identity check",
                                type.Name));
                    }

                    throw new EqualityOperatorValueCheckException(
                        string.Format("Expected type {0} == operator to returns result {1} for {2} == {3}",
                            type.Name, testCase.ExpectedResult, testCase.FirstInstance, testCase.SecondInstance));
                }
            }
        }
    }
}
