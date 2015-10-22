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
    }
}
