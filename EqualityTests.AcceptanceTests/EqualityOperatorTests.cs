using System.Reflection;
using EqualityTests.Assertions;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualityOperatorTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenEqualityOperatorProducesTheSameResultAsEquals(EqualityOperatorAssertion sut)
        {
            var exception = Record.Exception(
                () =>
                    sut.Verify(typeof (EqualityOperatorExample).GetMethod("op_Equality",
                        BindingFlags.Static | BindingFlags.Public)));

            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenEqualityOperatorDoNotProducesTheSameResultAsEquals(EqualityOperatorAssertion sut)
        {
            var exception = Record.Exception(
                () =>
                    sut.Verify(typeof (EqualityOperatorDifferFromEqualsExample).GetMethod("op_Equality",
                        BindingFlags.Static | BindingFlags.Public)));

            Assert.IsType<EqualityOperatorException>(exception);
        }

        [Theory, AutoData]
        public void ShouldExceptionMessageContainTypeName(EqualityOperatorAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (EqualityOperatorDifferFromEqualsExample).GetMethod("op_Equality",
                            BindingFlags.Static | BindingFlags.Public)));

            Assert.Contains(typeof (EqualityOperatorDifferFromEqualsExample).Name, exception.Message);
        }

        public class EqualityOperatorExample
        {
            public override bool Equals(object obj)
            {
                return true;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator ==(
                EqualityOperatorExample a,
                EqualityOperatorExample b
                )
            {
                return true;
            }

            public static bool operator !=(
                EqualityOperatorExample a,
                EqualityOperatorExample b
                )
            {
                return true;
            }
        }

        public class EqualityOperatorDifferFromEqualsExample
        {
            public override bool Equals(object obj)
            {
                return true;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public static bool operator ==(
                EqualityOperatorDifferFromEqualsExample a,
                EqualityOperatorDifferFromEqualsExample b
                )
            {
                return false;
            }

            public static bool operator !=(
                EqualityOperatorDifferFromEqualsExample a,
                EqualityOperatorDifferFromEqualsExample b
                )
            {
                return false;
            }
        }
    }
}
