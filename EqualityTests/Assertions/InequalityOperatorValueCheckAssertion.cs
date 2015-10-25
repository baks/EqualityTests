using System;
using System.Linq;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Assertions
{
    public class InequalityOperatorValueCheckAssertion : IdiomaticAssertion
    {
        private readonly IEqualityTestCaseProvider equalityTestCaseProvider;

        public InequalityOperatorValueCheckAssertion(IEqualityTestCaseProvider equalityTestCaseProvider)
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

            var inequalityOperator = type.GetInequalityOperatorMethod();

            foreach (var testCase in equalityTestCaseProvider.For(type))
            {
                var result =
                    (bool) inequalityOperator.Invoke(null, new[] {testCase.FirstInstance, testCase.SecondInstance});

                if (result == testCase.ExpectedResult)
                {
                    if (testCase.ExpectedResult == false)
                    {
                        throw new InequalityOperatorValueCheckException(
                            string.Format(
                                "Expected type {0} != operator to perform value check but looks like it performs identity check",
                                type.Name));
                    }

                    throw new InequalityOperatorValueCheckException(
                        string.Format("Expected type {0} != operator to returns result {1} for {2} == {3}",
                            type.Name, testCase.ExpectedResult, testCase.FirstInstance, testCase.SecondInstance));
                }
            }
        }
    }
}
