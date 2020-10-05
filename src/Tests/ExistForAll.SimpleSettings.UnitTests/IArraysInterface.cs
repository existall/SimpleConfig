namespace ExistForAll.SimpleSettings.UnitTests
{
	[SettingsSection]
	public interface IArraysInterface
	{
		[SettingsProperty(DefaultValue = 4)]
		int Number { get; set; }

		[SettingsProperty(DefaultValue = new [] {"x", "y"})]
		string[] Array { get; set; }

		[SettingsProperty(DefaultValue = new [] {1, 1})]
		int[] Array2 { get; set; }
	}
}