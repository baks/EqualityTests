using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public class EqualityTestCaseProvider : IEqualityTestCaseProvider
    {
        private readonly ISpecimenBuilder specimenBuilder;

        public EqualityTestCaseProvider(ISpecimenBuilder specimenBuilder)
        {
            if (specimenBuilder == null)
            {
                throw new ArgumentNullException("specimenBuilder");
            }
            this.specimenBuilder = specimenBuilder;
        }

        public IEnumerable<EqualityTestCase> For(Type type)
        {
            var tracker = new ConstructorArgumentsTracker(specimenBuilder, type.GetConstructors().Single());

            var instance = tracker.CreateNewInstance();
            var anotherInstance = tracker.CreateNewInstanceWithTheSameCtorArgsAsIn(instance);

            yield return new EqualityTestCase {FirstInstance = instance, SecondInstance = anotherInstance, ExpectedResult = true};

            foreach (var distinctInstance in tracker.CreateDistinctInstancesByChaningOneByOneCtorArgIn(instance))
            {
                yield return new EqualityTestCase {FirstInstance = instance, SecondInstance = distinctInstance, ExpectedResult = false};
            }
        }
    }
}