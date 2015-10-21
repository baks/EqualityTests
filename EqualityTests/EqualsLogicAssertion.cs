using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EqualityTests.Exception;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public class EqualsLogicAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public EqualsLogicAssertion(ISpecimenBuilder specimenBuilder)
        {
            if (specimenBuilder == null)
            {
                throw new ArgumentNullException("specimenBuilder");
            }
            this.specimenBuilder = specimenBuilder;
        }

        public override void Verify(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var tracker = new ConstructorArgumentsTracker(specimenBuilder, type.GetConstructors().Single());

            var instance = tracker.CreateNewInstance();
            var anotherInstance = tracker.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            var areEqual = instance.Equals(anotherInstance);

            if (areEqual == false)
            {
                throw new EqualsLogicException();
            }

            if (
                tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance)
                    .Any(distinctInstance => instance.Equals(distinctInstance)))
            {
                throw new EqualsLogicException();
            }
        }
    }
}
