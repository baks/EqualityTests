using EqualityTests.Assertions;
using EqualityTests.Exception;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class GetHashCodeValueCheckAssertionTests
    {
        [Theory, AutoTestData]
        public void ShouldNotThrowWhenGetHashCodeProducesHashBasedOnValues(GetHashCodeValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ValueObjectHashCodeExample>(sut);
        }

        [Theory, AutoTestData]
        public void ShouldThrowWhenGetHashCodeProducesHashBasedOnIdentity(GetHashCodeValueCheckAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType<GetHashCodeValueCheckException, IdentityObjectHashCodeExample>(sut);
        }

        [Theory, AutoTestData]
        public void ShouldExplainWhyExceptionIsThrownWhenHashCodeIsProducedOnIdentity(GetHashCodeValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof (IdentityObjectHashCodeExample)));

            Assert.Contains(
                string.Format(
                    "Expected type {0} GetHashCode method to compute bash based on value semantic not identity",
                    typeof(IdentityObjectHashCodeExample).Name), exception.Message);
        }
    }

    public class IdentityObjectHashCodeExample
    {
    }

    public class ValueObjectHashCodeExample
    {
        public ValueObjectHashCodeExample(int x)
        {
            X = x;
        }

        public int X { get; private set; }

        public override int GetHashCode()
        {
            return X.GetHashCode();
        }
    }
}
