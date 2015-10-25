using System;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;

namespace EqualityTests.Assertions
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
                throw new EqualsOverrideException(
                    string.Format("Expected type {0} to override Equals method", type.Name));
            }
        }
    }
}
