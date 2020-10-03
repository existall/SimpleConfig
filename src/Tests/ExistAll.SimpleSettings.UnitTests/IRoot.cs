namespace ExistAll.SimpleSettings.UnitTests
{
	public interface IRoot : ISettingsSection
	{
		string Value { get; set; }
	}
}