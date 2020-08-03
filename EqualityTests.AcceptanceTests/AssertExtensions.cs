using AutoFixture.Idioms;
using Xunit;

namespace EqualityTests.AcceptanceTests
{
    public static class EqualityTestAssert
    {
        public static void ExceptionWasNotThrownForTestType<T>(IdiomaticAssertion idiomaticAssertion)
        {
            var exception = Record.Exception(
                () => idiomaticAssertion.Verify(typeof(T)));

            Assert.Null(exception);
        }

        public static void ExceptionWasThrownForTestType<TException, TTestType>(IdiomaticAssertion sut)
        {
            var exception = Record.Exception(
                () => sut.Verify(typeof(TTestType)));

            Assert.IsType<TException>(exception);
        }
    }
}
