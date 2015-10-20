using System;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;

namespace EqualityTests
{
    public class InequalityOperatorOverloadAssertion : IdiomaticAssertion
    {
        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var inequalityOperatorOverload =
                type.GetMethod("op_Inequality", new [] {type, type});

            if (inequalityOperatorOverload == null)
            {
                throw new InequalityOperatorException(
                    string.Format("Expected type {0} to overload != operator with parameters of type {0}", type.Name));
            }
        }
    }
}
