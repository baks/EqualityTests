using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualityOperatorValueCheckTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowExceptionWhenEqualityOperatorPerformsValueCheck(
            EqualityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ClassWithEqualityOperatorValueCheck>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowExceptionWhenIdentityCheckInEqualityOperator(EqualityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <EqualityOperatorValueCheckException, ClassWithEqualityOperatorIdentityCheck>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowExceptionWhenEqualityOperatorReturnsDifferentResultThanEquals(
            EqualityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <EqualityOperatorValueCheckException, ClassWithEqualityOperatorThatDifferFromEquals>(sut);
        }

        public class ClassWithEqualityOperatorValueCheck
        {
            public ClassWithEqualityOperatorValueCheck(int x)
            {
                X = x;
            }

            public int X { get; private set; }

            public static bool operator ==(ClassWithEqualityOperatorValueCheck a, ClassWithEqualityOperatorValueCheck b)
            {
                return a.X == b.X;
            }

            public static bool operator !=(ClassWithEqualityOperatorValueCheck a, ClassWithEqualityOperatorValueCheck b)
            {
                return a.X != b.X;
            }
        }

        public class ClassWithEqualityOperatorIdentityCheck { }

        public class ClassWithEqualityOperatorThatDifferFromEquals
        {
            public ClassWithEqualityOperatorThatDifferFromEquals(int x)
            {
                X = x;
            }

            public int X { get; private set; }

            public override bool Equals(object obj)
            {
                return true;
            }

            public static bool operator ==(ClassWithEqualityOperatorThatDifferFromEquals a, ClassWithEqualityOperatorThatDifferFromEquals b)
            {
                return a.X == b.X;
            }

            public static bool operator !=(ClassWithEqualityOperatorThatDifferFromEquals a, ClassWithEqualityOperatorThatDifferFromEquals b)
            {
                return a.X != b.X;
            }
        }
    }
}
