using System;
using System.Linq;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public class EqualityOperatorValueCheckAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public EqualityOperatorValueCheckAssertion(ISpecimenBuilder specimenBuilder)
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

            var equalityOperator =
                type.GetMethod("op_Equality", new[] {type, type});

            var tracker = new ConstructorArgumentsTracker(specimenBuilder, type.GetConstructors().Single());

            var instance = tracker.CreateNewInstance();
            var anotherInstance = tracker.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            var equalityOperatorResult = (bool)equalityOperator.Invoke(null, new[] { instance, anotherInstance });

            if (equalityOperatorResult == false)
            {
                throw new EqualityOperatorValueCheckException(
                    string.Format(
                        "Expected type {0} == operator to perform value check but looks like it performs identity check",
                        type.Name));
            }

            foreach (var distinctInstance in tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance))
            {
                if (instance.Equals(distinctInstance) !=
                    (bool) equalityOperator.Invoke(null, new[] {instance, distinctInstance}))
                {
                    throw new EqualityOperatorValueCheckException(
                        string.Format("Expected type {0} == operator to returns the same results as Equals method",
                            type.Name));
                }
            }
        }
    }
}
