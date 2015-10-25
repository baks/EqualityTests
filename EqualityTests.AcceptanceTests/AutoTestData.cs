using EqualityTests.AcceptanceTests.Customizations;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace EqualityTests.AcceptanceTests
{
    public class AutoTestData : AutoDataAttribute
    {
        public AutoTestData() : base(new Fixture().Customize(new EqualityTestCaseProviderCustomization()))
        {

        }
    }
}
