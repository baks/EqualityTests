using EqualityTests.Assertions;
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
        public void ShouldExplainWhyExceptionIsThrownWhenIdentityCheck(EqualityOperatorValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof(ClassWithEqualityOperatorIdentityCheck)));

            Assert.Contains(
                string.Format(
                    "Expected type {0} == operator to perform value check but looks like it performs identity check",
                    typeof (ClassWithEqualityOperatorIdentityCheck).Name), exception.Message);
        }

        [Theory, AutoData]
        public void ShouldThrowExceptionWhenEqualityOperatorReturnsDifferentResultThanEquals(
            EqualityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <EqualityOperatorValueCheckException, ClassWithEqualityOperatorThatDifferFromEquals>(sut);
        }

        [Theory, AutoData]
        public void ShouldExplainWhyExceptionIsThrownWhenEqualityOperatorDiffersFromEqualsMethod(EqualityOperatorValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof(ClassWithEqualityOperatorThatDifferFromEquals)));

            Assert.Contains(string.Format("Expected type {0} == operator to returns the same results as Equals method",
                typeof (ClassWithEqualityOperatorThatDifferFromEquals).Name), exception.Message);
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

        public class ClassWithEqualityOperatorIdentityCheck
        {
            public static bool operator ==(ClassWithEqualityOperatorIdentityCheck a, ClassWithEqualityOperatorIdentityCheck b)
            {
                return ReferenceEquals(a, b);
            }

            public static bool operator !=(ClassWithEqualityOperatorIdentityCheck a, ClassWithEqualityOperatorIdentityCheck b)
            {
                return !ReferenceEquals(a, b);
            }
        }

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
