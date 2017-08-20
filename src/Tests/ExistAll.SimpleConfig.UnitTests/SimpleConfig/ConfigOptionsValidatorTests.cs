using System;
using ExistAll.SimpleConfig.Core;
using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{

	public class ConfigOptionsValidatorTests
	{
		[Theory]
		[InlineData(null,null,null)]
		[InlineData(null, null, "")]
		[InlineData(null, null, "  ")]
		public void ValidateOptions_WhenAttributeAndInterfaceAndSuffixAreMissing_ShouldThrowException(Type attributeType,
			Type interfaceType,
			string suffix)
		{
			var options = new ConfigOptions()
			{
				AttributeType = attributeType,
				InterfaceBase = interfaceType,
				ConfigSuffix = suffix
			};

			var sut = GetSut();

			Assert.Throws<ConfigOptionsArgumentNullException>(() => sut.ValidateOptions(options));
		}

		[Theory]
		[InlineData(null)]
		[InlineData(" ")]
		[InlineData("")]
		public void ValidateOptions_WhenHasNoArrayDelimiter_ShouldThrowException(string delimiter)
		{
			var options = new ConfigOptions()
			{
				ArraySplitDelimiter = delimiter
			};

			var sut = GetSut();

			Assert.Throws<ConfigOptionsArgumentMissingException>(() => sut.ValidateOptions(options));
		}

		[Theory]
		[InlineData(null)]
		[InlineData(" ")]
		[InlineData("")]
		public void ValidateOptions_WhenHasNoDateTimeFormat_ShouldThrowException(string format)
		{
			var options = new ConfigOptions()
			{
				DateTimeFormat = format
			};

			var sut = GetSut();

			Assert.Throws<ConfigOptionsArgumentMissingException>(() => sut.ValidateOptions(options));
		}

		[Fact]
		public void ValidateOptions_WhenHasNoSectionNameFormater_ShouldThrowException()
		{
			var options = new ConfigOptions
			{
				SectionNameFormater = null
			};
			var sut = GetSut();

			Assert.Throws<ConfigOptionsArgumentMissingException>(() => sut.ValidateOptions(options));
		}

		private ConfigOptionsValidator GetSut()
		{
			return new ConfigOptionsValidator();
		}
	}
}
