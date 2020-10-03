using ExistAll.SimpleSettings.UnitTests.SimpleSettings;

namespace ExistAll.SimpleSettings.UnitTests
{
	[SettingsSection]
	public interface IWithEnvironmentVariable
	{
		[SettingsProperty(EnvironmentVariableAttributeTests.EnvironmentVariable)]
		string EnvironmentVariable { get; set; }
	}
}