namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IEmailSenderConfig
	{
		[ConfigProperty(DefaultValue ="SomeUrl")] string EmailServiceUrl { get; set; }

		[ConfigProperty(DefaultValue = 3)] int Retries { get; set; }
	}
}