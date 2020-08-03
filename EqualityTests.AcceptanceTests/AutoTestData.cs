using EqualityTests.AcceptanceTests.Customizations;
using AutoFixture;
using AutoFixture.NUnit3;

namespace EqualityTests.AcceptanceTests
{
    public class AutoTestData : AutoDataAttribute
    {
        public AutoTestData() : base(new Fixture().Customize(new EqualityTestCaseProviderCustomization()))
        {

        }
    }
}
