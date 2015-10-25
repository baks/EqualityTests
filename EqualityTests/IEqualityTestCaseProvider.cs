using System;
using System.Collections.Generic;

namespace EqualityTests
{
    public interface IEqualityTestCaseProvider
    {
        IEnumerable<EqualityTestCase> For(Type type);
    }
}
