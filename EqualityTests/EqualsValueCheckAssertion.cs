using System;
using System.Linq;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public class EqualsValueCheckAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public EqualsValueCheckAssertion(ISpecimenBuilder specimenBuilder)
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
                throw new EqualsValueCheckException(
                    string.Format("Expected type {0} to perform value check but looks like it performs identity check",
                        type.Name));
            }

            foreach (var distinctInstance in tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance))
            {
                if (instance.Equals(distinctInstance))
                {
                    throw new EqualsValueCheckException(string.Format("Expected {0} to be not equal to {1}",
                        instance, distinctInstance));
                }
            }
        }
    }
}
