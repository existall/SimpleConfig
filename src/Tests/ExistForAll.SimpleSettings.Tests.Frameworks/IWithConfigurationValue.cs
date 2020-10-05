namespace ExistForAll.SimpleSettings.Tests.Frameworks
{
	[SettingsSection]
	public interface IWithConfigurationValue
	{
		[SettingsProperty("key")]
		string WithValue { get; set; }

		[SettingsProperty("noValue")]
		string WithoutValue { get; set; }
		
		string NonExistence { get; set; }
	}
}
