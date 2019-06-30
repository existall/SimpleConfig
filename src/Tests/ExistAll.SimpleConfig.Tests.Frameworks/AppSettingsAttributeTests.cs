using Xunit;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	public class AppSettingsAttributeTests
	{
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
        {
            var sut = ConfigBuilder.CreateBuilder();

			var result = sut.ScanAssemblies(GetType().Assembly);

			var config = result.GetConfig<IWithConfigurationValue>();

			Assert.Equal(config.WithValue, TestsConstants.AppSettingsValue);
			Assert.Equal(string.Empty, config.WithoutValue);
			Assert.Null(config.NonExistence);
		}
	}
}