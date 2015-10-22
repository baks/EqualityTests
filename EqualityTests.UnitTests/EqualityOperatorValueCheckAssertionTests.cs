using System;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class EqualityOperatorValueCheckAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(EqualityOperatorValueCheckAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(EqualityOperatorValueCheckAssertion).GetConstructors());
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckVerifyMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (EqualityOperatorValueCheckAssertion).GetMethod("Verify",
                new[] {typeof (Type)}));
        }
    }
}
