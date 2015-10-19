using System;
using System.Reflection;

namespace EqualityTests.Extensions
{
    public static class TypeExtensions
    {
        public static MethodInfo GetEqualsMethod(this Type type)
        {
            return type.GetMethod("Equals", new[] {typeof (object)});
        }
    }
}
