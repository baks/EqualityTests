using System;
using System.Collections.Generic;
using EqualityTests.Assertions;
using EqualityTests.Extensions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public static class EqualityTestsFor<T> where T : class
    {
        public static void Assert()
        {
            var fixture = new Fixture();
            var compositeAssertion =
                new CompositeIdiomaticAssertion(EqualityAssertions(fixture, new EqualityTestCaseProvider(fixture)));

            compositeAssertion.Verify(typeof(T));
            compositeAssertion.Verify(typeof(T).GetEqualsMethod());
            compositeAssertion.Verify(typeof(T).GetMethod("GetHashCode"));
        }

        public static void Assert(Func<EqualityTestsConfiguration<T>> configuration)
        {
            var fixture = new Fixture();
            var compositeAssertion =
                new CompositeIdiomaticAssertion(EqualityAssertions(fixture, configuration()));

            compositeAssertion.Verify(typeof(T));
            compositeAssertion.Verify(typeof(T).GetEqualsMethod());
            compositeAssertion.Verify(typeof(T).GetMethod("GetHashCode"));
        }

        private static IEnumerable<IdiomaticAssertion> EqualityAssertions(ISpecimenBuilder specimenBuilder,
            IEqualityTestCaseProvider equalityTestCaseProvider)
        {
            yield return new EqualsOverrideAssertion();
            yield return new GetHashCodeOverrideAssertion();
            yield return new EqualsSelfAssertion(specimenBuilder);
            yield return new EqualsSymmetricAssertion(specimenBuilder);
            yield return new EqualsTransitiveAssertion(specimenBuilder);
            yield return new EqualsSuccessiveAssertion(specimenBuilder);
            yield return new EqualsNullAssertion(specimenBuilder);
            yield return new EqualsValueCheckAssertion(equalityTestCaseProvider);
            //new GetHashCodeCorrectAssertion(),
            yield return new GetHashCodeSuccessiveAssertion(specimenBuilder);
            yield return new EqualityOperatorOverloadAssertion();
            yield return new InequalityOperatorOverloadAssertion();
            yield return new EqualityOperatorValueCheckAssertion(equalityTestCaseProvider);
            yield return new InequalityOperatorValueCheckAssertion(equalityTestCaseProvider);
            yield return new IEquatableImplementedAssertion();
            yield return new IEquatableValueCheckAssertion(equalityTestCaseProvider);
        }
    }
}
