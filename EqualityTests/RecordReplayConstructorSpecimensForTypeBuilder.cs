using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture.Kernel;

namespace EqualityTests
{
    public class RecordReplayConstructorSpecimensForTypeBuilder : ISpecimenBuilder
    {
        private readonly ISpecimenBuilder builder;
        private readonly IRequestSpecification requestFilter;
        public readonly List<object> recordedSpecimens;

        public RecordReplayConstructorSpecimensForTypeBuilder(ISpecimenBuilder builder, IRequestSpecification requestToRecordSpecification)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }
            if (requestToRecordSpecification == null)
            {
                throw new ArgumentNullException("requestToRecordSpecification");
            }
            this.builder = builder;
            this.requestFilter = requestToRecordSpecification;
            this.recordedSpecimens = new List<object>();
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (requestFilter.IsSatisfiedBy(request))
            {
                var constructor = GetConstructor(request);

                return constructor.Invoke(GetConstructorParameters(constructor, context).ToArray());
            }

            return builder.Create(request, context);
        }

        private ConstructorInfo GetConstructor(object request)
        {
            var type = request as Type;
            var constructor = type.GetConstructors().Single();

            return constructor;
        }

        private IEnumerable<object> GetConstructorParameters(ConstructorInfo constructor, ISpecimenContext context)
        {
            if (recordedSpecimens.Any())
            {
                return recordedSpecimens;
            } 

            var parameters = (from pi in constructor.GetParameters()
                select context.Resolve(pi)).ToList();

            if (recordedSpecimens.Count == 0)
            {
                recordedSpecimens.AddRange(parameters);
            }

            return parameters;
        }
    }
}
