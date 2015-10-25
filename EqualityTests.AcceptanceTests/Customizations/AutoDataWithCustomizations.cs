using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;

namespace EqualityTests.AcceptanceTests.Customizations
{
    public class AutoDataWithCustomizations : AutoDataAttribute
    {
        public AutoDataWithCustomizations(params Type[] customizationTypes)
        {
            foreach (var type in customizationTypes)
            {
                var customization = (ICustomization)Activator.CreateInstance(type);
                Fixture.Customize(customization);
            }
        }
    }
}
