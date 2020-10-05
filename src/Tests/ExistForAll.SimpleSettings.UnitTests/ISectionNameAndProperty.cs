namespace ExistForAll.SimpleSettings.UnitTests
{
	[SettingsSection(Name = "section-root")]
	public interface ISectionNameAndProperty
	{
		[SettingsProperty(Name = "section-value")]
		string Value { get; set; }
	}
}