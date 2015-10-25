using System.Reflection;
using EqualityTests.Assertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class EqualityOperatorAssertionTests
    {
        [Theory, AutoData]
        public void ShouldBeIdiomaticAssertion(EqualityOperatorAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(EqualityOperatorAssertion).GetConstructors());
        }

        public class EqualityOperatorAssertion_VerifyMethod
        {
            [Theory, AutoData]
            public void ShouldGuardCheckArguments(
                [Frozen] Fixture fixture,
                GuardClauseAssertion guardClauseAssertion)
            {
                fixture.Inject(typeof (EqualityOperatorAssertion).GetMethod("Equals", BindingFlags.Public));

                guardClauseAssertion.Verify(typeof (EqualityOperatorAssertion).GetMethod("Verify",
                    new[] {typeof (MethodInfo)}));
            }
        }
    }
}
