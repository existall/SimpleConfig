using System.Reflection;
using Xunit;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	public class AppSettingsAttributeTests
	{
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
		{
			var sut = new ConfigBuilder();

			var configCollection = sut.Build(new[] { GetType().GetTypeInfo().Assembly });
			var config = configCollection.GetConfig<IWithConfigurationValue>();

			Assert.Equal(config.WithValue, TestsConstants.AppSettingsValue);
			//Assert.Null(config.WithoutValue);
		}
	}
}