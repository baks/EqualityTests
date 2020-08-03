using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;

namespace EqualityTests.UnitTests
{
    public class AutoDomainData : AutoDataAttribute
    {
        public AutoDomainData() : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {

        }
    }
}
