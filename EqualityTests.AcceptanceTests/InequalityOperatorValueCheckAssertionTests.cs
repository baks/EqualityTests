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
        public void ShouldThrowWhenInequalityOperatorPerformsIdentityCheck(InequalityOperatorOverloadAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <InequalityOperatorValueCheckException, ClassWithInequalityOperatorIdentityCheck>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenInequalityOperatorsDiffersFromEqualsResults(InequalityOperatorOverloadAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <InequalityOperatorException, ClassWithInequalityOperatorThatDifferFromEqualsMethod>(sut);
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

        public class ClassWithInequalityOperatorIdentityCheck { }

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
