using System;
using ExistAll.SimpleConfig.Convertion;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigBuilderConversionsTests
	{

		[Fact]
		public void Build_WhenAddGlobalConverter_ShouldReturnConverterValue()
		{
			var sut = new ConfigBuilder();
			sut.AddTypeConverter(new GuidConfigConvertor());

			var result = sut.Build(new [] { typeof(IGuidInterface) });
			var config = result.GetConfig<IGuidInterface>();

			Assert.NotEqual(Guid.Empty, config.Guid);
		}

		[Fact]
		public void Build_WhenAddLocalConverter_ShouldReturnConverterValue()
		{
			var sut = new ConfigBuilder();
			
			var result = sut.Build(new[] { typeof(IGuidInterfaceWithConversionAttribute) });
			var config = result.GetConfig<IGuidInterfaceWithConversionAttribute>();

			Assert.NotEqual(Guid.Empty, config.Guid);
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

