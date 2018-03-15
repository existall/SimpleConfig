using ExistAll.SimpleConfig.DotNet.Frameworks;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	[ConfigSection]
	public interface IWithConfigurationValue
	{
		[AppSettingsValue(TestsConstants.AppSettingsKeyWithValue)]
		string WithValue { get; set; }

		[AppSettingsValue(TestsConstants.AppSettingsKeyWithoutValue)]
		string WithoutValue { get; set; }

		[AppSettingsValue("")]
		string NonExistence { get; set; }

	}
}
