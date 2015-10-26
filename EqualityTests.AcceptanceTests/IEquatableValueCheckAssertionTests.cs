using System;
using EqualityTests.Assertions;
using EqualityTests.Exception;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class IEquatableValueCheckAssertionTests
    {
        [Theory, AutoTestData]
        public void ShouldThrowWhenIEquatableEqualsPerformsIdentityCheck(IEquatableValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasThrownForTestType<IEquatableValueCheckException, IEquatableWithIdentityCheck>
                (sut);
        }

        [Theory, AutoTestData]
        public void ShouldExplainWhyExceptionIsThrownWhenIEquatablePerformsIdentityCheck(IEquatableValueCheckAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof(IEquatableWithIdentityCheck)));

            Assert.Contains(
                string.Format(
                    "Expected IEquatable<{0}>.Equals method to perform value check but looks like it performs identity check",
                    typeof(IEquatableWithIdentityCheck).Name), exception.Message);
        }

        [Theory, AutoTestData]
        public void ShouldNotThrowWhenIEquatablePerformsValueCheck(IEquatableValueCheckAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<IEquatableWithValueCheck>(sut);
        }
    }

    public class IEquatableWithValueCheck : IEquatable<IEquatableWithValueCheck>
    {
        public IEquatableWithValueCheck(int x)
        {
            X = x;
        }

        public int X { get; private set; }

        public bool Equals(IEquatableWithValueCheck other)
        {
            return this.X == other.X;
        }
    }

    public class IEquatableWithIdentityCheck : IEquatable<IEquatableWithIdentityCheck>
    {
        public bool Equals(IEquatableWithIdentityCheck other)
        {
            return ReferenceEquals(this, other);
        }
    }
}
