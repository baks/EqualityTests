using System;
using System.Linq;
using EqualityTests.Exception;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.Assertions
{
    public class InequalityOperatorValueCheckAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public InequalityOperatorValueCheckAssertion(ISpecimenBuilder specimenBuilder)
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

            var inequalityOperator =
                type.GetMethod("op_Inequality", new[] { type, type });

            var tracker = new ConstructorArgumentsTracker(specimenBuilder, type.GetConstructors().Single());

            var instance = tracker.CreateNewInstance();
            var anotherInstance = tracker.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            var inequalityOperatorResult = (bool)inequalityOperator.Invoke(null, new[] { instance, anotherInstance });

            if (inequalityOperatorResult)
            {
                throw new InequalityOperatorValueCheckException(
                    string.Format(
                        "Expected type {0} != operator to perform value check but looks like it performs identity check",
                        type.Name));
            }

            foreach (var distinctInstance in tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance))
            {
                if (instance.Equals(distinctInstance) ==
                    (bool)inequalityOperator.Invoke(null, new[] { instance, distinctInstance }))
                {
                    throw new InequalityOperatorValueCheckException(string.Format(
                        "Expected type {0} != operator to returns equivalent results as Equals method", type.Name));
                }
            }
        }
    }
}
