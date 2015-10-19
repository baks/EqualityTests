using System.Reflection;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public class EqualsTransitiveTests
    {
        [Theory, AutoData]
        public void ShouldNotThrowWhenEqualsImplementationIsTransitive(EqualsTransitiveAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (TransitiveEqualsExample).GetMethod("Equals",
                            BindingFlags.Public | BindingFlags.Instance)));
            Assert.Null(exception);
        }

        [Theory, AutoData]
        public void ShouldThrowWhenEqualsImplementationIsNotTransitive(EqualsTransitiveAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (NotTransitiveEqualsExample).GetMethod("Equals",
                            BindingFlags.Public | BindingFlags.Instance)));

            Assert.IsType<EqualsTransitiveException>(exception);
        }

        [Theory, AutoData]
        public void ShouldExceptionMessageContainTypeName(EqualsTransitiveAssertion sut)
        {
            var exception =
                Record.Exception(
                    () =>
                        sut.Verify(typeof (NotTransitiveEqualsExample).GetMethod("Equals",
                            BindingFlags.Public | BindingFlags.Instance)));

            Assert.Contains(typeof (NotTransitiveEqualsExample).Name, exception.Message);
        }

        public class NotTransitiveEqualsExample
        {
            private object iAmOnlyEqualToThisOneInstance;

            public NotTransitiveEqualsExample(int x, int y)
            {
            }

            public override bool Equals(object obj)
            {
                if (iAmOnlyEqualToThisOneInstance == null)
                {
                    iAmOnlyEqualToThisOneInstance = obj;
                    return true;
                }
                return false;
            }
        }

        public class TransitiveEqualsExample
        {
            public override bool Equals(object obj)
            {
                return true;
            }
        }
    }
}
