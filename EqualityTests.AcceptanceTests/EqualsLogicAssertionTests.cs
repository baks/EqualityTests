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
