using System.Collections.Generic;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public static class EqualityTestsFor<T> where T : class
    {
        public static void Assert()
        {
            var compositeAssertion = new CompositeIdiomaticAssertion(EqualityAssertions(new Fixture()));

            compositeAssertion.Verify(typeof(T));
            compositeAssertion.Verify(typeof(T).GetMethod("Equals", new[] { typeof(object) }));
            compositeAssertion.Verify(typeof(T).GetMethod("GetHashCode"));
        }

        private static IEnumerable<IdiomaticAssertion> EqualityAssertions(ISpecimenBuilder specimenBuilder)
        {
            yield return new EqualsOverrideAssertion();
            yield return new GetHashCodeOverrideAssertion();
            yield return new EqualsSelfAssertion(specimenBuilder);
            yield return new EqualsSymmetricAssertion(specimenBuilder);
            yield return new EqualsTransitiveAssertion(specimenBuilder);
            yield return new EqualsSuccessiveAssertion(specimenBuilder);
            yield return new EqualsNullAssertion(specimenBuilder);
            yield return new EqualsValueCheckAssertion(specimenBuilder);
            //new GetHashCodeCorrectAssertion(),
            yield return new GetHashCodeSuccessiveAssertion(specimenBuilder);
            yield return new EqualityOperatorOverloadAssertion();
            yield return new InequalityOperatorOverloadAssertion();
            yield return new EqualityOperatorValueCheckAssertion(specimenBuilder);
            yield return new InequalityOperatorValueCheckAssertion(specimenBuilder);
            yield return new IEquatableImplementedAssertion();
            //new IEquatableCorrectAssertion()
        }
    }
}
