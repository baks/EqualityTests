using EqualityTests.Assertions;
using EqualityTests.Exception;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualityOperatorValueCheckTests
    {
        [Theory, AutoTestData]
        public void ShouldNotThrowExceptionWhenEqualityOperatorPerformsValueCheck(
            EqualityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ClassWithEqualityOperatorValueCheck>(sut);
        }

        [Theory, AutoTestData]
        public void ShouldThrowExceptionWhenIdentityCheckInEqualityOperator(EqualityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <EqualityOperatorValueCheckException, ClassWithEqualityOperatorIdentityCheck>(sut);
        }

        [Theory, AutoTestData]
        public void ShouldExplainWhyExceptionIsThrownWhenIdentityCheck(EqualityOperatorValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof(ClassWithEqualityOperatorIdentityCheck)));

            Assert.Contains(
                string.Format(
                    "Expected type {0} == operator to perform value check but looks like it performs identity check",
                    typeof (ClassWithEqualityOperatorIdentityCheck).Name), exception.Message);
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
    }
}
