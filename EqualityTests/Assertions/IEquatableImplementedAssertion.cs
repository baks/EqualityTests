using System;
using System.Linq;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;

namespace EqualityTests.Assertions
{
    public class IEquatableImplementedAssertion : IdiomaticAssertion
    {
        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var isIEquatableOfType = new Func<Type, bool>(t => IsSystemIEquatableInterface(t, type));

            var equatableInterface =
                type.GetInterfaces().SingleOrDefault(isIEquatableOfType);

            if (equatableInterface == null)
            {
                throw new IEquatableImplementedException(string.Format(
                    "Expected type {0} to implement IEquatable<{0}>", type.Name));
            }
        }

        private static bool IsSystemIEquatableInterface(Type type, Type param)
        {
            return type.Namespace == "System" && type.Name == "IEquatable`1" &&
                   type.GenericTypeArguments.First() == param;
        }
    }
}
