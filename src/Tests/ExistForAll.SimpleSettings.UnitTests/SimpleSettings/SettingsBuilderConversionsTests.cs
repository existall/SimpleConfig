using System;
using ExistForAll.SimpleSettings.Conversion;
using Xunit;

namespace ExistForAll.SimpleSettings.UnitTests.SimpleSettings
{
	public class SettingsBuilderConversionsTests
	{
		[Fact]
		public void Build_WhenAddGlobalConverter_ShouldReturnConverterValue()
        {
            var sut = SettingsBuilder.CreateBuilder(x => x.AddTypeConverter(new GuidSettingsConvertor()));
            var result = sut.GetSettings<IGuidInterface>();
            
			Assert.NotEqual(Guid.Empty, result.Guid);
		}

		[Fact]
		public void Build_WhenAddLocalConverter_ShouldReturnConverterValue()
        {
            var sut = SettingsBuilder.CreateBuilder();
            var result = sut.GetSettings<IGuidInterfaceWithConversionAttribute>();

			Assert.NotEqual(Guid.Empty, result.Guid);
		}

		public interface IGuidInterface
		{
			[EmptyGuid]
			Guid Guid { get; set; }
		}

		public interface IGuidInterfaceWithConversionAttribute
		{
			[EmptyGuid(ConverterType = typeof(GuidSettingsConvertor))]
			Guid Guid { get; set; }
		}

		private class EmptyGuidAttribute : SettingsPropertyAttribute
		{
			public EmptyGuidAttribute()
				: base()
			{
				DefaultValue = Guid.Empty;
			}
		}

		private class GuidSettingsConvertor : ISettingsTypeConverter
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

