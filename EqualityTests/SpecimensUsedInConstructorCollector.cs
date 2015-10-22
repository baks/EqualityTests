using System;
using System.Collections.Generic;
using System.Linq;

namespace EqualityTests
{
    public class SpecimensUsedInConstructorCollector
    {
        private readonly Dictionary<object, IEnumerable<object>> ctorInstancesArguments;

        public SpecimensUsedInConstructorCollector()
        {
            this.ctorInstancesArguments = new Dictionary<object, IEnumerable<object>>();
        }

        public void AddSpecimens(object instance, object[] specimens)
        {
            ctorInstancesArguments.Add(instance, specimens);
        }

        public object[] GetSpecimens(object instance)
        {
            CheckIfCollectorContainsInstance(instance);

            return ctorInstancesArguments[instance].ToArray();
        }

        private void CheckIfCollectorContainsInstance(object obj)
        {
            if (!ctorInstancesArguments.ContainsKey(obj))
            {
                throw new InvalidOperationException(
                    string.Format("Collector does not contain specimens for instance {0}", obj));
            }
        }
    }
}
