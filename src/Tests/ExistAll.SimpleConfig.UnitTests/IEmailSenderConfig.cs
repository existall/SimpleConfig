namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IEmailSenderConfig
	{
		[DefaultValue("SomeUrl")] string EmailServiceUrl { get; set; }

		[DefaultValue(3)] int Retries { get; set; }
	}
}