using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace EqualityTests.UnitTests
{
    public class AutoDomainData : AutoDataAttribute
    {
        public AutoDomainData() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {

        }
    }
}
