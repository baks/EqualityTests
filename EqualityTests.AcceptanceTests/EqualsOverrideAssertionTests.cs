using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualsOverrideAssertionTests
    {
        [Theory, AutoData]
        public void ShouldThrowWhenClassDoesNotOverloadEqualsMethod(EqualsOverrideAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof (ClassWhichDoesNotOverrideEqualsMethod)));

            Assert.IsType<EqualsOverrideException>(exception);
        }

        [Theory, AutoData]
        public void ShouldNotThrowWhenClassOverloadEqualsMethod(EqualsOverrideAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof (ClassWichOverrideEqualsMethod)));

            Assert.Null(exception);
        }
    }

    public class ClassWhichDoesNotOverrideEqualsMethod { }

    public class ClassWichOverrideEqualsMethod
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
