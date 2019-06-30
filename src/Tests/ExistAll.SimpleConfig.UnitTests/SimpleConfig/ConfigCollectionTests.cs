using System;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
    public class ConfigCollectionTests
    {
        private const string SomeName = "SomeName";
		
        [Fact]
        public void GetConfig_WhenTypeIsNotInterface_ShouldThrowException()
        {
            var sut = ConfigBuilder.CreateBuilder();

            Assert.Throws<InvalidOperationException>(() => sut.GetConfig<ConfigBuilder>());
        }

        [Fact]
        public void GetConfig_BuildingInterfaceNotFromAssembly_ShouldReturnInstanceWithValues()
        {
            var sut = ConfigBuilder.CreateBuilder();

            var result = sut.GetConfig<ISomeInterface>();
			
            Assert.Equal(result.Name,SomeName);
        }

        public interface ISomeInterface
        {
            [DefaultValue(SomeName)]
            string Name { get; set; }
        }
		
    }
}