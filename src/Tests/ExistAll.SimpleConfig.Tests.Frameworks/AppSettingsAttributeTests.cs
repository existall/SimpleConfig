using Xunit;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	public class AppSettingsAttributeTests
	{
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var sut = ConfigBuilder.CreateBuilder()
				.AddAssembly(GetType().Assembly);

			var result = sut.Build();

			var config = result.GetConfig<IWithConfigurationValue>();

			Assert.Equal(config.WithValue, TestsConstants.AppSettingsValue);
			Assert.Equal(string.Empty, config.WithoutValue);
			Assert.Null(config.NonExistence);
		}
	}
}