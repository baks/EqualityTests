using System.Reflection;
using EqualityTests.Assertions;
using NSubstitute;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Idioms;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class EqualsSymmetricAssertionTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeIdiomaticAssertion(EqualsSymmetricAssertion sut)
        {
            Assert.IsAssignableFrom<IdiomaticAssertion>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(EqualsSymmetricAssertion).GetConstructors());
        }

        public class EqualsSymmetricAssertionTests_VerifyMethod
        {
            [Theory, AutoDomainData]
            public void ShouldGuardCheckArguments(
                [Frozen]Fixture fixture,
                GuardClauseAssertion guardClauseAssertion)
            {
                fixture.Inject(typeof(EqualsSymmetricAssertion).GetMethod("Equals", BindingFlags.Public));

                guardClauseAssertion.Verify(typeof(EqualsSymmetricAssertion).GetMethod("Verify",
                    new[] { typeof(MethodInfo) }));
            }
        }
    }
}
