using EqualityTests.Assertions;
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
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ClassWhichOverloadsEqualityOperator>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenEqualityOperatorIsNotOverloaded(EqualityOperatorOverloadAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType<EqualityOperatorException, ClassWhichDoesNotOverloadsEqualityOperator>(
                    sut);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenEqualityOperatorIsOverloadedWithArgumentsOtherThanContainingType(
            EqualityOperatorOverloadAssertion sut)
        {
            EqualityTestAssert.ExceptionWasThrownForTestType
                <EqualityOperatorException, ClassThatOverloadsEqualityOperatorWithArgumentsOtherThanContainingType>(sut);
        }

        [Theory, AutoData]
        public void ShouldContainTypeInExceptionMessage(EqualityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof (ClassWhichDoesNotOverloadsEqualityOperator)));

            Assert.Contains(typeof (ClassWhichDoesNotOverloadsEqualityOperator).Name, exception.Message);
        }

        [Theory, AutoData]
        public void ShouldContainParametersTypeInExceptionMessage(EqualityOperatorOverloadAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof(ClassWhichDoesNotOverloadsEqualityOperator)));

            Assert.Contains(
                string.Format("with parameters of type {0}", typeof (ClassWhichDoesNotOverloadsEqualityOperator).Name),
                exception.Message);
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

        public class ClassThatOverloadsEqualityOperatorWithArgumentsOtherThanContainingType
        {
            public static bool operator ==(ClassThatOverloadsEqualityOperatorWithArgumentsOtherThanContainingType a, string b)
            {
                return true;
            }

            public static bool operator !=(ClassThatOverloadsEqualityOperatorWithArgumentsOtherThanContainingType a, string b)
            {
                return true;
            }
        }
    }
}
