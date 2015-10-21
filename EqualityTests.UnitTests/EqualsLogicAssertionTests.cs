using System;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class EqualsLogicAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(EqualsLogicAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(EqualsLogicAssertion).GetConstructors());
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckVerifyMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (EqualsLogicAssertion).GetMethod("Verify", new[] {typeof (Type)}));
        }
    }
}
