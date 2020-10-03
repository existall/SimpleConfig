using Xunit;

namespace ExistAll.SimpleSettings.UnitTests.SimpleSettings
{
	public class SettingsPropertyTests
	{
		[Fact]
		public void Build_WhenAllowEmptyIsFalse_ShouldThrowException()
		{
			var sut = SettingsBuilder.CreateBuilder();

			Assert.Throws<SettingsPropertyValueException>(() => sut.GetSettings<IWithNonNullInterface>());
		}

		public interface IWithNonNullInterface
		{
			[SettingsProperty(AllowEmpty = false)]
			int Value { get; set; }
		}
	}
}
