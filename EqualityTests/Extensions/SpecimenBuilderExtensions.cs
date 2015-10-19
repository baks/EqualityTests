using System;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Extensions
{
    public static class SpecimenBuilderExtensions
    {
        public static object CreateInstanceOfType(this ISpecimenBuilder builder, Type type)
        {
            return builder.Create(type, new SpecimenContext(builder));
        }
    }
}
