using System;
using ExistAll.SimpleConfig.Conversion;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigBuilderConversionsTests
	{
		[Fact]
		public void Build_WhenAddGlobalConverter_ShouldReturnConverterValue()
        {
            var sut = ConfigBuilder.CreateBuilder(x => x.AddTypeConverter(new GuidConfigConvertor()));
            var result = sut.GetConfig<IGuidInterface>();
            
			Assert.NotEqual(Guid.Empty, result.Guid);
		}

		[Fact]
		public void Build_WhenAddLocalConverter_ShouldReturnConverterValue()
        {
            var sut = ConfigBuilder.CreateBuilder();
            var result = sut.GetConfig<IGuidInterfaceWithConversionAttribute>();

			Assert.NotEqual(Guid.Empty, result.Guid);
		}

		public interface IGuidInterface
		{
			[EmptyGuid]
			Guid Guid { get; set; }
		}

		public interface IGuidInterfaceWithConversionAttribute
		{
			[EmptyGuid, ConfigProperty(ConvertorType = typeof(GuidConfigConvertor))]
			Guid Guid { get; set; }
		}

		private class EmptyGuidAttribute : DefaultValueAttribute
		{
			public EmptyGuidAttribute()
				: base(Guid.Empty)
			{
			}
		}

		private class GuidConfigConvertor : IConfigTypeConverter
		{
			public bool CanConvert(Type configType)
			{
				return configType == typeof(Guid);
			}

			public object Convert(object value, Type configType)
			{
				return Guid.NewGuid();
			}
		}
	}
}

