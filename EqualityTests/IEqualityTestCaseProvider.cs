using System;
using System.Collections.Generic;

namespace EqualityTests
{
    public interface IEqualityTestCaseProvider
    {
        IEnumerable<TestCase> TestCasesFor(Type type);
    }
}
