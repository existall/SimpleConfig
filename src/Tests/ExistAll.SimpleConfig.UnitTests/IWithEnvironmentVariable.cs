using ExistAll.SimpleConfig.UnitTests.SimpleConfig;

namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IWithEnvironmentVariable
	{
		[ConfigProperty(EnvironmentVariableAttributeTests.EnvironmentVariable)]
		string EnvironmentVariable { get; set; }
	}
}