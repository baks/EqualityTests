using System;
using System.Reflection;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;

namespace EqualityTests
{
    public class EqualityOperatorOverloadAssertion : IdiomaticAssertion
    {
        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var equalityOperatorOverload = 
                type.GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static);

            if (equalityOperatorOverload == null)
            {
                throw new EqualityOperatorException(
                    string.Format("Expected type {0} to overload == operator", type.Name));
            }
        }
    }
}
