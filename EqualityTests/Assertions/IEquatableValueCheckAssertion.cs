using System;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;

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
                        throw new IEquatableValueCheckException();
                    }

                    throw new IEquatableValueCheckException();
                }
            }
        }
    }
}
