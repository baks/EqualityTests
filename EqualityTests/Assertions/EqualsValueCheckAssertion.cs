using System;
using System.Collections.Generic;
using System.Linq;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Assertions
{
    public class EqualityTestCaseProvider : IEqualityTestCaseProvider
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public EqualityTestCaseProvider(ISpecimenBuilder specimenBuilder)
        {
            this.specimenBuilder = specimenBuilder;
        }

        public IEnumerable<TestCase> TestCasesFor(Type type)
        {
            var tracker = new ConstructorArgumentsTracker(specimenBuilder, type.GetConstructors().Single());

            var instance = tracker.CreateNewInstance();
            var anotherInstance = tracker.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            yield return new TestCase {Example = instance, Against = anotherInstance, ExpectedResult = true};

            foreach (var distinctInstance in tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance))
            {
                yield return new TestCase {Example = instance, Against = distinctInstance, ExpectedResult = false};
            }
        }
    }

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

            foreach (var testCase in equalityTestCaseProvider.TestCasesFor(type))
            {
                var result = testCase.Example.Equals(testCase.Against);

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
                        testCase.Example, testCase.Against));
                }
            }
        }
    }
}
