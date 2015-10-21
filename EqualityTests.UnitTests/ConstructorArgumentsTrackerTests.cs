using System;
using System.Linq;
using NSubstitute;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class ConstructorArgumentsTrackerTests
    {
        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (ConstructorArgumentsTracker).GetConstructors().Single());
        }

        [Theory, AutoDomainData]
        public void ShouldCreateNewInstanceCreateParameters(
            [Substitute]ISpecimenBuilder specimenBuilder)
        {
            var sut = new ConstructorArgumentsTracker(specimenBuilder, typeof (SimpleType).GetConstructors().Single());

            sut.CreateNewInstance();

            specimenBuilder.Received(1).Create(Arg.Is(typeof (int)), Arg.Any<ISpecimenContext>());
        }

        [Theory, AutoDomainData]
        public void ShouldCreateInstanceWithTheSameCtorArgs(
            [Substitute]ISpecimenBuilder specimenBuilder)
        {
            var sut = new ConstructorArgumentsTracker(specimenBuilder,
                typeof (decimal).GetConstructor(new[] {typeof (double) }));

            var instance = sut.CreateNewInstance();
            var newInstance = sut.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            Assert.False(ReferenceEquals(instance, newInstance));
            Assert.True(instance.Equals(newInstance));
        }

        [Theory, AutoDomainData]
        public void ShouldThrowWhenPassedInstanceWasNotCreatedByTracker(
            ISpecimenBuilder specimenBuilder)
        {
            var sut = new ConstructorArgumentsTracker(specimenBuilder, typeof (SimpleType).GetConstructors().Single());

            var exception = Record.Exception(() => sut.CreateNewInstanceWithTheSameCtorArgsAsIn(new object()));

            Assert.IsType<InvalidOperationException>(exception);
        }

        [Theory, AutoDomainData]
        public void ShouldExplainWhyCannotCreateInstanceWhichWasNotTrackedByTracker(
            ISpecimenBuilder specimenBuilder)
        {
            var sut = new ConstructorArgumentsTracker(specimenBuilder, typeof (SimpleType).GetConstructors().Single());

            var instance = new object();
            var exception = Record.Exception(() => sut.CreateNewInstanceWithTheSameCtorArgsAsIn(instance));

            Assert.Equal(string.Format("Instance {0} was not created within tracker", instance), exception.Message);
        }

        [Theory, AutoDomainData]
        public void ShouldCreateAsManyDistinctInstancesAsCtorParameters(
            [Substitute]ISpecimenBuilder specimenBuilder)
        {
            specimenBuilder.Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>()).Returns(1);

            var sut = new ConstructorArgumentsTracker(specimenBuilder, typeof(SimpleType).GetConstructors().Single());

            var instance = sut.CreateNewInstance() as SimpleType;
            specimenBuilder.ClearReceivedCalls();

            var instances = sut.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance).ToList();

            specimenBuilder.Received(1).Create(Arg.Is(typeof(int)), Arg.Any<ISpecimenContext>());
            Assert.Equal(1, instances.Count);
        }
    }
}
