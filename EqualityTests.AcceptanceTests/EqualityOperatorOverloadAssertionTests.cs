using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualityOperatorOverloadAssertionTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenEqualityOperatorIsOverloaded(EqualityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof (ClassWhichOverloadsEqualityOperator)));

            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenEqualityOperatorIsNotOverloaded(EqualityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof(ClassWhichDoesNotOverloadsEqualityOperator)));

            Assert.IsType<EqualityOperatorException>(exception);
        }

        [Theory, AutoData]
        public void ShouldContainTypeInExceptionMessage(EqualityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof(ClassWhichDoesNotOverloadsEqualityOperator)));

            Assert.Contains(typeof(ClassWhichDoesNotOverloadsEqualityOperator).Name, exception.Message);
        }

        public class ClassWhichOverloadsEqualityOperator
        {
            public static bool operator ==(ClassWhichOverloadsEqualityOperator a, ClassWhichOverloadsEqualityOperator b)
            {
                return true;
            }

            public static bool operator !=(ClassWhichOverloadsEqualityOperator a, ClassWhichOverloadsEqualityOperator b)
            {
                return true;
            }
        }

        public class ClassWhichDoesNotOverloadsEqualityOperator { }
    }
}
