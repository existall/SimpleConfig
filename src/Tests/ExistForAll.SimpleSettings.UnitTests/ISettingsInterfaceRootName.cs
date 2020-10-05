namespace ExistForAll.SimpleSettings.UnitTests
{
	[SettingsSection(Name = "some-root-name")]
	public interface ISettingsInterfaceRootName
	{
		int Value { get; set; }
	}
}