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
