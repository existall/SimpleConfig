using System;
using ExistAll.SimpleSettings.Core;
using Xunit;

namespace ExistAll.SimpleSettings.UnitTests.SimpleSettings
{

	public class SettingsOptionsValidatorTests
	{
		[Theory]
		[InlineData(null,null,null)]
		[InlineData(null, null, "")]
		[InlineData(null, null, "  ")]
		public void ValidateOptions_WhenAttributeAndInterfaceAndSuffixAreMissing_ShouldThrowException(Type attributeType,
			Type interfaceType,
			string suffix)
		{
			var options = new SettingsOptions()
			{
				AttributeType = attributeType,
				InterfaceBase = interfaceType,
				SettingsSuffix = suffix
			};

			var sut = GetSut();

			Assert.Throws<SettingsOptionsArgumentNullException>(() => sut.ValidateOptions(options));
		}

        [Fact]
        public void ValidateOptions_WhenAttributeTypeIsNotAnAttribute_ShouldThrowException()
        {
            var options = new SettingsOptions()
            {
                AttributeType = typeof(NotAttribute)
            };

            var sut = GetSut();

            Assert.Throws<SettingsOptionNonAttributeException>(() => sut.ValidateOptions(options));
        }

        [Fact]
        public void ValidateOptions_WhenAttributeTypeIAnAttribute_ShouldPassValidation()
        {
            var options = new SettingsOptions()
            {
                AttributeType = typeof(SomeAttribute)
            };

            var sut = GetSut();

            sut.ValidateOptions(options);
        }

        [Theory]
		[InlineData(null)]
		[InlineData(" ")]
		[InlineData("")]
		public void ValidateOptions_WhenHasNoArrayDelimiter_ShouldThrowException(string delimiter)
		{
			var options = new SettingsOptions()
			{
				ArraySplitDelimiter = delimiter
			};

			var sut = GetSut();

			Assert.Throws<SettingsOptionsArgumentMissingException>(() => sut.ValidateOptions(options));
		}

		[Theory]
		[InlineData(null)]
		[InlineData(" ")]
		[InlineData("")]
		public void ValidateOptions_WhenHasNoDateTimeFormat_ShouldThrowException(string format)
		{
			var options = new SettingsOptions()
			{
				DateTimeFormat = format
			};

			var sut = GetSut();

			Assert.Throws<SettingsOptionsArgumentMissingException>(() => sut.ValidateOptions(options));
		}

		[Fact]
		public void ValidateOptions_WhenHasNoSectionNameFormater_ShouldThrowException()
		{
			var options = new SettingsOptions
			{
				SectionNameFormatter = null
			};
			var sut = GetSut();

			Assert.Throws<SettingsOptionsArgumentMissingException>(() => sut.ValidateOptions(options));
		}

		private SettingsOptionsValidator GetSut()
		{
			return new SettingsOptionsValidator();
		}

        private class SomeAttribute : Attribute
        {

        }

        private class NotAttribute
        {

        }
	}
}
