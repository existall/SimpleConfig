using ExistForAll.SimpleSettings.UnitTests.SimpleSettings;

namespace ExistForAll.SimpleSettings.UnitTests
{
	[SettingsSection]
	public interface IWithEnvironmentVariable
	{
		[SettingsProperty(EnvironmentVariableAttributeTests.EnvironmentVariable)]
		string EnvironmentVariable { get; set; }
	}
}