using System;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;

namespace EqualityTests
{
    public class EqualsOverrideAssertion : IdiomaticAssertion
    {
        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var equalsMethod = type.GetEqualsMethod();

            if (equalsMethod.IsObjectEqualsMethod())
            {
                throw new EqualsOverrideException();
            }
        }
    }
}
