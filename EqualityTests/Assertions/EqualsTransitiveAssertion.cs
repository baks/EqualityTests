using System;
using System.Reflection;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Assertions
{
    public class EqualsTransitiveAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder builder;

        public EqualsTransitiveAssertion(ISpecimenBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            this.builder = builder;
        }

        public override void Verify(MethodInfo methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }

            if (methodInfo.ReflectedType == null && !methodInfo.IsObjectEqualsOverrideMethod())
            {
                return;
            }

            var recordReplayBuilder = new RecordReplayConstructorSpecimensForTypeBuilder(builder,
                new ExactTypeSpecification(methodInfo.ReflectedType));

            var firstInstance = recordReplayBuilder.CreateInstanceOfType(methodInfo.ReflectedType);
            var secondInstance = recordReplayBuilder.CreateInstanceOfType(methodInfo.ReflectedType);
            var thirdInstance = recordReplayBuilder.CreateInstanceOfType(methodInfo.ReflectedType);

            var firstToSecondComparisonResult = firstInstance.Equals(secondInstance);
            var secondToThirdComparisonResult = secondInstance.Equals(firstInstance);
            var firstToThirdComparisonResult = firstInstance.Equals(thirdInstance);

            if ((firstToSecondComparisonResult && secondToThirdComparisonResult) != true)
            {
                throw new EqualsTransitiveException(
                    "Can't check transitive property of Equals implementation due to object created with the same values doesn't result true, propably they are performing identity check instead of value check");
            }

            if ((firstToSecondComparisonResult && secondToThirdComparisonResult) != firstToThirdComparisonResult)
            {
                throw new EqualsTransitiveException(string.Format(
                    "Equals implementation of type {0} is not transitive. It breaks following rule x.Equals(y) && y.Equals(z) == true then x.Equals(z) == true",
                    methodInfo.ReflectedType));
            }
        }
    }
}
