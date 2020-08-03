using System;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using AutoFixture.Idioms;

namespace EqualityTests.Assertions
{
    public class EqualityOperatorOverloadAssertion : IdiomaticAssertion
    {
        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var equalityOperatorOverload = type.GetEqualityOperatorMethod();

            if (equalityOperatorOverload == null)
            {
                throw new EqualityOperatorException(
                    string.Format("Expected type {0} to overload == operator with parameters of type {0}", type.Name));
            }
        }
    }
}
