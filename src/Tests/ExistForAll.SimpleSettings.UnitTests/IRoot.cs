namespace ExistForAll.SimpleSettings.UnitTests
{
	public interface IRoot : ISettingsSection
	{
		string Value { get; set; }
	}
}