using EqualityTests.Assertions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace EqualityTests.AcceptanceTests.Customizations
{
    public class EqualityTestCaseProviderCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<IEqualityTestCaseProvider>(
                composer => composer.FromFactory(() => new EqualityTestCaseProvider(fixture.Create<ISpecimenBuilder>())));
        }
    }
}
