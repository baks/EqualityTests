using System;
using System.Linq;
using AutoFixture.Kernel;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class SpecimensUsedInConstructorCollectorTests
    {
        [Theory, AutoDomainData]
        public void ShouldThrowWhenPassedInstanceWasNotCollected(
            SpecimensUsedInConstructorCollector sut)
        {
            var exception = Record.Exception(() => sut.GetSpecimens(new object()));

            Assert.IsType<InvalidOperationException>(exception);
        }

        [Theory, AutoDomainData]
        public void ShouldExplainWhyCannotCreateInstanceWhichWasNotTrackedByTracker(
            SpecimensUsedInConstructorCollector sut)
        {
            var instance = new object();
            var exception = Record.Exception(() => sut.GetSpecimens(instance));

            Assert.Equal(string.Format("Collector does not contain specimens for instance {0}", instance),
                exception.Message);
        }
    }
}
