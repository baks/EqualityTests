using System;
using Ploeh.AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class IEquatableImplementedAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(IEquatableImplementedAssertion sut)
        {
            Assert.IsAssignableFrom<IEquatableImplementedAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldVerifyMethodGuardCheckArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (IEquatableImplementedAssertion).GetMethod("Verify",
                new[] {typeof (Type)}));
        }
    }
}
