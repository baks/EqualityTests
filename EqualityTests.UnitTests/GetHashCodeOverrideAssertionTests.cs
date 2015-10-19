using System;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class GetHashCodeOverrideAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(GetHashCodeOverrideAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckVerifyMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(
                typeof (GetHashCodeOverrideAssertion).GetMethod("Verify", new[] {typeof (Type)}));
        }
    }
}
