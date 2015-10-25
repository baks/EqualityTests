using EqualityTests.AcceptanceTests.Customizations;
using EqualityTests.Assertions;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualsValueCheckAssertionTests
    {
        [Theory, AutoDataWithCustomizations(typeof (EqualityTestCaseProviderCustomization))]
        public void ShouldThrowWhenIdentityCheckInEqualsImplementation(EqualsValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasThrownForTestType<EqualsValueCheckException, object>(sut);
        }

        [Theory, AutoDataWithCustomizations(typeof(EqualityTestCaseProviderCustomization))]
        public void ShouldExplainWhyExceptionIsThrownWhenEqualsIsIdentityCheck(EqualsValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (object)));

            Assert.Equal(
                string.Format("Expected type {0} to perform value check but looks like it performs identity check", typeof (object).Name),
                exception.Message);
        }

        [Theory, AutoDataWithCustomizations(typeof(EqualityTestCaseProviderCustomization))]
        public void ShouldNotThrowWhenValueCheckInEqualsImplementation(EqualsValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ValueObjectExample>(sut);
        }

        [Theory, AutoDataWithCustomizations(typeof(EqualityTestCaseProviderCustomization))]
        public void ShouldThrowWhenNotEveryCtorArgumentInfluenceEquality(EqualsValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <EqualsValueCheckException, ValueObjectButSecondCtrArgDoesntTakePartInEqualsImpl>(sut);
        }

        [Theory, AutoDataWithCustomizations(typeof(EqualityTestCaseProviderCustomization))]
        public void ShouldExplainWhyExceptionIsThrownWhenCtorArgDoesNotInfluenceEquality(EqualsValueCheckAssertion sut)
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

        public class ValueObjectWhichTakesOtherValueObjectInCtor
        {
            public ValueObjectWhichTakesOtherValueObjectInCtor(ValueObjectExample valueObject)
            {
                ValueObject = valueObject;
            }

            public ValueObjectExample ValueObject { get; private set; }

            public override bool Equals(object obj)
            {
                var vo = obj as ValueObjectWhichTakesOtherValueObjectInCtor;

                return this.ValueObject.Equals(vo.ValueObject);
            }
        }
    }
}
