namespace ExistForAll.SimpleSettings.UnitTests
{
	[SettingsSection]
	public interface IWithoutEnvironmentVariable
	{
		string Path { get; set; }
	}
}