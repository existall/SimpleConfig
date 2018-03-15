using Xunit;

namespace ExistAll.SimpleConfig.UnitTests.SimpleConfig
{
	public class ConfigPropertyTests
	{
		[Fact]
		public void Build_WhenAllowEmptyIsFalse_ShouldThrowException()
		{
			var sut = new ConfigBuilder();

			Assert.Throws<ConfigPropertyValueException>(() => sut.Build(new[] {typeof(IWithNonNullInterface)}));
		}

		public interface IWithNonNullInterface
		{
			[ConfigProperty(AllowEmpty = false)]
			int Value { get; set; }
		}
	}
}
