using System;
using EqualityTests.Assertions;
using AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class GetHashCodeValueCheckAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(GetHashCodeValueCheckAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (GetHashCodeValueCheckAssertion).GetConstructors());
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckVerifyMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (GetHashCodeValueCheckAssertion).GetMethod("Verify",
                new[] {typeof (Type)}));
        }
    }
}
