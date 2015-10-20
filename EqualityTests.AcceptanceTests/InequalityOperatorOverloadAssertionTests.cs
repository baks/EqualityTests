using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class InequalityOperatorOverloadAssertionTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenInequalityOperatorIsOverloaded(InequalityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ClassWhichOverloadInequalityOperator)));

            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenInequalityOperatorIsNotOverloaded(InequalityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ClassWhichDoesNotOverloadInequalityOperator)));

            Assert.IsType<InequalityOperatorException>(exception);
        }

        [Theory, AutoData]
        public void ShouldExceptionMessageContainTypeName(InequalityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ClassWhichDoesNotOverloadInequalityOperator)));

            Assert.Contains(typeof(ClassWhichDoesNotOverloadInequalityOperator).Name, exception.Message);
        }

        public class ClassWhichOverloadInequalityOperator
        {
            public static bool operator ==(ClassWhichOverloadInequalityOperator a, ClassWhichOverloadInequalityOperator b)
            {
                return true;
            }

            public static bool operator !=(ClassWhichOverloadInequalityOperator a, ClassWhichOverloadInequalityOperator b)
            {
                return true;
            }
        }

        public class ClassWhichDoesNotOverloadInequalityOperator { }
    }
}
