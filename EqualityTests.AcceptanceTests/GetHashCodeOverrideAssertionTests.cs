using EqualityTests.Assertions;
using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class GetHashCodeOverrideAssertionTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenTypeOverridesGetHashCodeMethod(GetHashCodeOverrideAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ClassWhichOverrideGetHashCodeMethod)));

            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenTypeDoesNotOverrideGetHashCodeMethod(GetHashCodeOverrideAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ClassWhichDoesNotOverrideGetHashCodeMethod)));

            Assert.IsType<GetHashCodeOverrideException>(exception);
        }

        [Theory, AutoData]
        public void ShouldContainTypeNameInExceptionMessage(GetHashCodeOverrideAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ClassWhichDoesNotOverrideGetHashCodeMethod)));

            Assert.Contains(typeof(ClassWhichDoesNotOverrideGetHashCodeMethod).Name ,exception.Message);
        }

        public class ClassWhichOverrideGetHashCodeMethod
        {
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public class ClassWhichDoesNotOverrideGetHashCodeMethod { }
    }
}
