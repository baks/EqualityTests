using System;
using System.Reflection;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Assertions
{
    public class EqualityOperatorAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder builder;

        public EqualityOperatorAssertion(ISpecimenBuilder builder)
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

            if (methodInfo.ReflectedType == null && !methodInfo.IsEqualityOperator())
            {
                return;
            }

            var firstInstance = builder.CreateInstanceOfType(methodInfo.ReflectedType);
            var secondInstance = builder.CreateInstanceOfType(methodInfo.ReflectedType);

            var equalsResult = firstInstance.Equals(secondInstance);

            var equalityOperatorResult = (bool)methodInfo.Invoke(null, new[] {firstInstance, secondInstance});

            if (equalsResult != equalityOperatorResult)
            {
                throw new EqualityOperatorException(string.Format(
                    "Equality operator for type {0} returns {1} where Equals method for that type returns {2}",
                    methodInfo.ReflectedType, equalityOperatorResult, equalsResult));
            }
        }
    }
}
