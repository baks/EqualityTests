using System;
using EqualityTests.Assertions;
using AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class InequalityOperatorOverloadAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(InequalityOperatorOverloadAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckVerifyMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(
                typeof(InequalityOperatorOverloadAssertion).GetMethod("Verify", new [] { typeof(Type) }));
        }
    }
}
