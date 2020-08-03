using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EqualityTests.Extensions;
using AutoFixture.Kernel;

namespace EqualityTests
{
    public class ConstructorArgumentsTracker
    {
        private readonly ISpecimenBuilder specimenBuilder;
        private readonly ConstructorInfo constructorInfo;
        private readonly SpecimensUsedInConstructorCollector collector;

        public ConstructorArgumentsTracker(ISpecimenBuilder specimenBuilder, ConstructorInfo constructorInfo)
        {
            if (specimenBuilder == null)
            {
                throw new ArgumentNullException("specimenBuilder");
            }
            if (constructorInfo == null)
            {
                throw new ArgumentNullException("constructorInfo");
            }

            this.specimenBuilder = specimenBuilder;
            this.constructorInfo = constructorInfo;
            this.collector = new SpecimensUsedInConstructorCollector();
        }

        public object CreateNewInstance()
        {
            var parameters = (from pi in constructorInfo.GetParameters()
                              select specimenBuilder.CreateInstanceOfType(pi.ParameterType)).ToList();

            var instance = constructorInfo.Invoke(parameters.ToArray());

            collector.AddSpecimens(instance, parameters.ToArray());

            return instance;
        }

        public object CreateNewInstanceWithTheSameCtorArgsAsIn(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return constructorInfo.Invoke(collector.GetSpecimens(obj));
        }

        public IEnumerable<object> CreateDistinctInstancesByChaningOneByOneCtorArgIn(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return DistinctInstancesFor(obj);
        }

        private IEnumerable<object> DistinctInstancesFor(object obj)
        {
            var arguments = collector.GetSpecimens(obj);

            for (var idx = 0; idx < arguments.Length; idx++)
            {
                yield return constructorInfo.Invoke(arguments.Select(
                    (o, i) => i == idx ? specimenBuilder.CreateInstanceOfType(o.GetType()) : o).ToArray());
            }
        }
    }
}
