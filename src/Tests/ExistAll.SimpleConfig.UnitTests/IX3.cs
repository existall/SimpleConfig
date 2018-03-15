namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection(Name = "dude")]
	public interface IX3
	{
		int Age { get; set; }
	}
}