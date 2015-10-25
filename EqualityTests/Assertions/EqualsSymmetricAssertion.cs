using System;
using System.Reflection;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Assertions
{
    public class EqualsSymmetricAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder builder;

        public EqualsSymmetricAssertion(ISpecimenBuilder builder)
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

            var firstInstance = builder.CreateInstanceOfType(methodInfo.ReflectedType);
            var secondInstance = builder.CreateInstanceOfType(methodInfo.ReflectedType);

            var firstComparisonResult = firstInstance.Equals(secondInstance);
            var secondComparisonResult = secondInstance.Equals(firstInstance);

            if (firstComparisonResult != secondComparisonResult)
            {
                throw new EqualsSymmetricException(
                    string.Format(
                        "Equals implementation of type {0} is not symmetric. x.Equals(y) returns {1} but y.Equals(x) return {2}",
                        methodInfo.ReflectedType, firstComparisonResult, secondComparisonResult));
            }
        }
    }
}
