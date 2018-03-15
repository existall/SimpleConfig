namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection(Name = "father")]
	public interface IX4
	{
		[ConfigProperty(Name = "mother")] int Age { get; set; }
	}
}