using System;
using System.Collections.Generic;

namespace EqualityTests
{
    public class EqualityTestsConfiguration<T> : IEqualityTestCaseProvider where T : class
    {
        private readonly T instance;
        private readonly IList<EqualityTestCase> testCases; 

        public EqualityTestsConfiguration(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            this.instance = instance;
            this.testCases = new List<EqualityTestCase>();
        }

        public EqualityTestsConfiguration<T> ShouldBeEqualTo(T obj)
        {
            testCases.Add(new EqualityTestCase(instance, obj, true));
            return this;
        }

        public EqualityTestsConfiguration<T> ShouldNotBeEqualTo(T obj)
        {
            testCases.Add(new EqualityTestCase(instance, obj, false));
            return this;
        }

        public IEnumerable<EqualityTestCase> For(Type type)
        {
            if (type != typeof (T))
            {
                throw new InvalidOperationException("");
            }

            return testCases;
        }
    }
}