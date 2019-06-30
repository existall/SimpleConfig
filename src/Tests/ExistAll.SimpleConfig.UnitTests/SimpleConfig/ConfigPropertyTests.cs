using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigPropertyTests
	{
		[Fact]
		public void Build_WhenAllowEmptyIsFalse_ShouldThrowException()
		{
			var sut = ConfigBuilder.CreateBuilder();

			Assert.Throws<ConfigPropertyValueException>(() => sut.GetConfig<IWithNonNullInterface>());
		}

		public interface IWithNonNullInterface
		{
			[ConfigProperty(AllowEmpty = false)]
			int Value { get; set; }
		}
	}
}
