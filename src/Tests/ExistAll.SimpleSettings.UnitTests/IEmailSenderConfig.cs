namespace ExistAll.SimpleSettings.UnitTests
{
	[SettingsSection]
	public interface IEmailSenderConfig
	{
		[SettingsProperty(DefaultValue ="SomeUrl")] string EmailServiceUrl { get; set; }

		[SettingsProperty(DefaultValue = 3)] int Retries { get; set; }
	}
}