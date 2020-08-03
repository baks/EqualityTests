using System;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using AutoFixture.Idioms;

namespace EqualityTests.Assertions
{
    public class IEquatableValueCheckAssertion : IdiomaticAssertion
    {
        private readonly IEqualityTestCaseProvider equalityTestCaseProvider;

        public IEquatableValueCheckAssertion(IEqualityTestCaseProvider equalityTestCaseProvider)
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

            var equalsFromIEquatable = type.GetStronglyTypedEqualsMethod();

            foreach (var testCase in equalityTestCaseProvider.For(type))
            {
                var result = (bool) equalsFromIEquatable.Invoke(testCase.FirstInstance, new[] {testCase.SecondInstance});

                if (result != testCase.ExpectedResult)
                {
                    if (testCase.ExpectedResult)
                    {
                        throw new IEquatableValueCheckException(
                            string.Format(
                                "Expected IEquatable<{0}>.Equals method to perform value check but looks like it performs identity check",
                                type.Name));
                    }

                    throw new IEquatableValueCheckException(
                        string.Format(
                            "Expected IEquatable<{0}>.Equals method return {1}, but {2} was returned for {3} {4}",
                            type.Name, testCase.ExpectedResult, !testCase.ExpectedResult,
                            testCase.FirstInstance, testCase.SecondInstance));
                }
            }
        }
    }
}
