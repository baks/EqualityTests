using System.Reflection;
using System.Threading;
using EqualityTests.Assertions;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualsSymmetricTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenEqualsIsIdentityCheckAndIsSymmetric(EqualsSymmetricAssertion sut)
        {
            var exception =
                Record.Exception(
                    () => sut.Verify(typeof (object).GetMethod("Equals", BindingFlags.Public | BindingFlags.Instance)));
            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldNotThrowWhenValueObjectEqualsImplementationIsSymmetric(EqualsSymmetricAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (ValueObjectSymmetricEqualsImplementationExample).GetMethod("Equals",
                            BindingFlags.Public | BindingFlags.Instance)));
            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenEqualsImplementationIsNotSymmetric(EqualsSymmetricAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (NotSymmetricEqualsImplementationExample).GetMethod("Equals",
                            BindingFlags.Public | BindingFlags.Instance)));

            Assert.IsType<EqualsSymmetricException>(exception);
        }

        [Theory, AutoData]
        public void ShouldExceptionMessageContainTypeName(EqualsSymmetricAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (NotSymmetricEqualsImplementationExample).GetMethod("Equals",
                            BindingFlags.Public | BindingFlags.Instance)));

            Assert.Contains(typeof (NotSymmetricEqualsImplementationExample).Name, exception.Message);
        }

        public class NotSymmetricEqualsImplementationExample
        {
            private static int typeCounter = -1;
            private readonly int instanceCounter;

            public NotSymmetricEqualsImplementationExample()
            {
                instanceCounter = Interlocked.Increment(ref typeCounter);
            }

            public override bool Equals(object obj)
            {
                return instanceCounter % 2 == 0;
            }
        }

        public class ValueObjectSymmetricEqualsImplementationExample
        {
            private readonly int x;

            public ValueObjectSymmetricEqualsImplementationExample(int x)
            {
                this.x = x;
            }

            public override bool Equals(object obj)
            {
                var other = obj as ValueObjectSymmetricEqualsImplementationExample;

                return x == other.x;
            }
        }
    }
}
