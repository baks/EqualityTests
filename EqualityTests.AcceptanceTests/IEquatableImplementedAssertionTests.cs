using System;
using EqualityTests.Assertions;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class IEquatableImplementedAssertionTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowExceptionWhenIEquatableIsImplemented(IEquatableImplementedAssertion sut)
        {
            EqualityTestAssert.ExceptionWasNotThrownForTestType<ClassThatImplementsIEquatable>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowExceptionWhenIEquatableIsNotImplemented(IEquatableImplementedAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType<IEquatableImplementedException, ClassWhichDoesNotImplementIEquatable>(sut);
        }

        [Theory, AutoData]
        public void ShouldThrowExceptionWhenIEquatableIsImplementedWithoutTypeThatImplementsInterface(IEquatableImplementedAssertion sut)
        {
            EqualityTestAssert
                .ExceptionWasThrownForTestType
                <IEquatableImplementedException, ClassThatImplementsIEquatableWithOtherType>(sut);
        }

        [Theory, AutoData]
        public void ShouldExceptionMessageContainTypeName(IEquatableImplementedAssertion sut)
        {
            var exception = Record.Exception(() => sut.Verify(typeof (ClassWhichDoesNotImplementIEquatable)));

            Assert.Contains(typeof (ClassWhichDoesNotImplementIEquatable).Name, exception.Message);
        }

        public class ClassThatImplementsIEquatable : IEquatable<ClassThatImplementsIEquatable> 
        {
            public bool Equals(ClassThatImplementsIEquatable other)
            {
                throw new NotImplementedException();
            }
        }

        public class ClassWhichDoesNotImplementIEquatable { }

        public class ClassThatImplementsIEquatableWithOtherType : IEquatable<string>
        {
            public bool Equals(string other)
            {
                throw new NotImplementedException();
            }
        }
    }
}
