using System;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class InequalityOperatorValueCheckAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(InequalityOperatorValueCheckAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (InequalityOperatorValueCheckAssertion).GetConstructors());
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckVerifyMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (InequalityOperatorValueCheckAssertion).GetMethod("Verify",
                new[] {typeof (Type)}));
        }
    }
}
