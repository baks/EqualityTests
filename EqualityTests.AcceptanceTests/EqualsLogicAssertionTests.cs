using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualsLogicAssertionTests
    {
        [Theory, AutoData]
        public void ShouldThrowWhenIdentityCheckInEqualsImplementation(EqualsLogicAssertion sut)
        {
            EqualityTestAssert.ExceptionWasThrownForTestType<EqualsLogicException, object>(sut);
        }

        [Theory, AutoData]
        public void ShouldExplainWhyExceptionIsThrownWhenEqualsIsIdentityCheck(EqualsLogicAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (object)));

            Assert.Equal(
                string.Format("Expected type {0} to perform value check but looks like it performs identity check", typeof (object).Name),
                exception.Message);
        }

        [Theory, AutoData]
        public void ShouldNotThrowWhenValueCheckInEqualsImplementation(EqualsLogicAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ValueObjectExample>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenNotEveryCtorArgumentInfluenceEquality(EqualsLogicAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <EqualsLogicException, ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl>(sut);
        }

        [Theory, AutoData]
        public void ShouldExplainWhyExceptionIsThrownWhenCtorArgDoesNotInfluenceEquality(EqualsLogicAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl)));

            Assert.Equal(
                string.Format("Expected {0} to be not equal to {0}",
                    new ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl(1,1)), exception.Message);
        }

        public class ValueObjectExample
        {
            public ValueObjectExample(int x)
            {
                X = x;
            }

            public int X { get; private set; }

            public override bool Equals(object obj)
            {
                var vo = obj as ValueObjectExample;

                return this.X == vo.X;
            }
        }

        public class ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl
        {
            public ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl(int x, int y)
            {
                X = x;
            }

            public int X { get; private set; }

            public override bool Equals(object obj)
            {
                var vo = obj as ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl;

                return this.X == vo.X;
            }
        }
    }
}
