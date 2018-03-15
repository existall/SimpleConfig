namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IWithoutEnvironmentVariable
	{
		string Path { get; set; }
	}
}