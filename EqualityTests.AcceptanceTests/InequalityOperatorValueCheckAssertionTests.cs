using EqualityTests.Assertions;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class InequalityOperatorValueCheckAssertionTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenInequalityOperatorPerformsValueCheck(InequalityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ClassWithInequalityOperatorValueCheck>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenInequalityOperatorPerformsIdentityCheck(InequalityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <InequalityOperatorValueCheckException, ClassWithInequalityOperatorIdentityCheck>(sut);
        }

        [Theory, AutoData]
        public void ShouldExplainWhyExceptionIsThrownWhenInequalityOperatorPerformsIdentityCheck(InequalityOperatorValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof(ClassWithInequalityOperatorIdentityCheck)));

            Assert.Contains(
                string.Format(
                    "Expected type {0} != operator to perform value check but looks like it performs identity check",
                    typeof (ClassWithInequalityOperatorIdentityCheck).Name), exception.Message);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenInequalityOperatorsDiffersFromEqualsResults(InequalityOperatorValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <InequalityOperatorValueCheckException, ClassWithInequalityOperatorThatDifferFromEqualsMethod>(sut);
        }

        [Theory, AutoData]
        public void ShouldExplainWhyExceptionIsThrownWhenInequalityOperatorDifferFromEqualsResults(InequalityOperatorValueCheckAssertion sut)
        {
            var exception = Record.Exception(
    () => sut.Verify(typeof(ClassWithInequalityOperatorThatDifferFromEqualsMethod)));

            Assert.Contains(
                string.Format(
                    "Expected type {0} != operator to returns equivalent results as Equals method",
                    typeof (ClassWithInequalityOperatorThatDifferFromEqualsMethod).Name), exception.Message);
        }

        public class ClassWithInequalityOperatorValueCheck
        {
            public ClassWithInequalityOperatorValueCheck(int x)
            {
                X = x;
            }

            public int X { get; private set; }

            public static bool operator ==(ClassWithInequalityOperatorValueCheck a, ClassWithInequalityOperatorValueCheck b)
            {
                return a.X == b.X;
            }

            public static bool operator !=(ClassWithInequalityOperatorValueCheck a, ClassWithInequalityOperatorValueCheck b)
            {
                return a.X != b.X;
            }
        }

        public class ClassWithInequalityOperatorIdentityCheck
        {
            public static bool operator ==(ClassWithInequalityOperatorIdentityCheck a, ClassWithInequalityOperatorIdentityCheck b)
            {
                return ReferenceEquals(a, b);
            }

            public static bool operator !=(ClassWithInequalityOperatorIdentityCheck a, ClassWithInequalityOperatorIdentityCheck b)
            {
                return !ReferenceEquals(a, b);
            }
        }

        public class ClassWithInequalityOperatorThatDifferFromEqualsMethod
        {
            public ClassWithInequalityOperatorThatDifferFromEqualsMethod(int x)
            {
                X = x;
            }

            public int X { get; private set; }

            public override bool Equals(object obj)
            {
                return true;
            }

            public static bool operator ==(ClassWithInequalityOperatorThatDifferFromEqualsMethod a, ClassWithInequalityOperatorThatDifferFromEqualsMethod b)
            {
                return a.X == b.X;
            }

            public static bool operator !=(ClassWithInequalityOperatorThatDifferFromEqualsMethod a, ClassWithInequalityOperatorThatDifferFromEqualsMethod b)
            {
                return a.X != b.X;
            }
        }
    }
}
