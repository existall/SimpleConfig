using System;
using Xunit;

namespace ExistAll.SimpleSettings.UnitTests.SimpleSettings
{
    public class SettingsCollectionTests
    {
        private const string SomeName = "SomeName";
		
        [Fact]
        public void GetSettings_WhenTypeIsNotInterface_ShouldThrowException()
        {
            var sut = SettingsBuilder.CreateBuilder();

            Assert.Throws<InvalidOperationException>(() => sut.GetSettings<SettingsBuilder>());
        }

        [Fact]
        public void GetSettings_BuildingInterfaceNotFromAssembly_ShouldReturnInstanceWithValues()
        {
            var sut = SettingsBuilder.CreateBuilder();

            var result = sut.GetSettings<ISomeInterface>();
			
            Assert.Equal(result.Name,SomeName);
        }

        public interface ISomeInterface
        {
            [SettingsProperty(DefaultValue = SomeName)]
            string Name { get; set; }
        }
		
    }
}