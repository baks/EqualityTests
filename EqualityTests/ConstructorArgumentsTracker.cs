using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EqualityTests.Extensions;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests
{
    public class ConstructorArgumentsTracker
    {
        private readonly ISpecimenBuilder specimenBuilder;
        private readonly ConstructorInfo constructorInfo;
        private readonly Dictionary<object, IEnumerable<object>> ctorInstancesArguments;

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
            this.ctorInstancesArguments = new Dictionary<object, IEnumerable<object>>();
        }

        public object CreateNewInstance()
        {
            var parameters = (from pi in constructorInfo.GetParameters()
                              select specimenBuilder.CreateInstanceOfType(pi.ParameterType)).ToList();

            var instance = constructorInfo.Invoke(parameters.ToArray());
            ctorInstancesArguments.Add(instance, parameters);

            return instance;
        }

        public object CreateNewInstanceWithTheSameCtorArgsAsIn(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            CheckIfInstanceIsTracked(obj);

            return constructorInfo.Invoke(ctorInstancesArguments[obj].ToArray());
        }

        public IEnumerable<object> CreateDistinctInstancesByChaningOneByOneCtorArgIn(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            CheckIfInstanceIsTracked(obj);

            return DistinctInstancesFor(obj);
        }

        private void CheckIfInstanceIsTracked(object obj)
        {
            if (!ctorInstancesArguments.ContainsKey(obj))
            {
                throw new InvalidOperationException(string.Format("Instance {0} was not created within tracker", obj));
            }
        }

        private IEnumerable<object> DistinctInstancesFor(object obj)
        {
            var arguments = ctorInstancesArguments[obj].ToList();

            for (var idx = 0; idx < arguments.Count; idx++)
            {
                yield return constructorInfo.Invoke(arguments.Select(
                    (o, i) => i == idx ? specimenBuilder.CreateInstanceOfType(o.GetType()) : o).ToArray());
            }
        }
    }
}
