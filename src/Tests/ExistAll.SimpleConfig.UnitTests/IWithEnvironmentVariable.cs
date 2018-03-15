namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IWithEnvironmentVariable
	{
		[EnvironmentVariable("Path")] string Path { get; set; }
	}
}