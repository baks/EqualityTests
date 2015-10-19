using EqualityTests.Extensions;
using NSubstitute;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace EqualityTests.UnitTests
{
    public class RecordReplayConstructorSpecimensForTypeBuilderTests
    {
        [Theory, AutoDomainData]
        public void ShouldBeSpecimenBuilder(RecordReplayConstructorSpecimensForTypeBuilder sut)
        {
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckConstructorArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof(RecordReplayConstructorSpecimensForTypeBuilder).GetConstructors());
        }

        [Theory, AutoDomainData]
        public void ShouldGuardCheckCreateMethodArguments(GuardClauseAssertion guardClauseAssertion)
        {
            guardClauseAssertion.Verify(typeof (RecordReplayConstructorSpecimensForTypeBuilder).GetMethod("Create"));
        }

        [Theory, AutoDomainData]
        public void ShouldDelegateToDecoratedBuilderIfFilterIsNotSatisfied(
            [Frozen]IRequestSpecification requestSpecification,
            [Substitute]ISpecimenBuilder builder)
        {
            requestSpecification.IsSatisfiedBy(Arg.Any<object>()).Returns(false);

            var sut = new RecordReplayConstructorSpecimensForTypeBuilder(builder, requestSpecification);
            sut.CreateInstanceOfType(typeof (object));

            builder.Received(1).Create(Arg.Any<object>(), Arg.Any<ISpecimenContext>());
        }

        [Theory, AutoDomainData]
        public void ShouldCreateConstructorParametersIfFilterIsSatisfied(
            [Frozen]IRequestSpecification requestSpecification,
            ISpecimenContext specimenContext,
            RecordReplayConstructorSpecimensForTypeBuilder sut)
        {
            requestSpecification.IsSatisfiedBy(Arg.Any<object>()).Returns(true);

            sut.Create(typeof (SimpleType), specimenContext);

            specimenContext.Received(1).Resolve(Arg.Any<object>());
        }

        [Theory, AutoDomainData]
        public void ShouldUseRecordedConstructorSpecimensInSubsequentCallsForTheSameRequest(
            [Frozen]IRequestSpecification requestSpecification,
            ISpecimenContext context,
            RecordReplayConstructorSpecimensForTypeBuilder sut)
        {
            requestSpecification.IsSatisfiedBy(Arg.Any<object>()).Returns(true);

            sut.Create(typeof (SimpleType), context);
            sut.Create(typeof (SimpleType), context);

            context.Received(1).Resolve(Arg.Any<object>());
        }
    }
}
