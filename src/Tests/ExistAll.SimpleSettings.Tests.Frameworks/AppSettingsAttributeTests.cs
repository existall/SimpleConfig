using ExistAll.SimpleSettings.DotNet.Frameworks;
using Xunit;

namespace ExistAll.SimpleSettings.Tests.Frameworks
{
	public class AppSettingsAttributeTests
	{
		[Fact]
		public void Build_WhereVariableHasValue_ShouldSetProperty()
        {
            var sut = SettingsBuilder.CreateBuilder(x => x.AddAppSettings());

			var result = sut.ScanAssemblies(GetType().Assembly);

			var settings = result.GetSettings<IWithConfigurationValue>();

			Assert.Equal(settings.WithValue, TestsConstants.AppSettingsValue);
			Assert.Equal(string.Empty, settings.WithoutValue);
			Assert.Null(settings.NonExistence);
		}
	}
}