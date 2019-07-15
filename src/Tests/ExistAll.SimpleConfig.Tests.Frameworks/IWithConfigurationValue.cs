using ExistAll.SimpleConfig.DotNet.Frameworks;

namespace ExistAll.SimpleConfig.Tests.Frameworks
{
	[ConfigSection]
	public interface IWithConfigurationValue
	{
		[ConfigProperty("key")]
		string WithValue { get; set; }

		[ConfigProperty("noValue")]
		string WithoutValue { get; set; }
		
		string NonExistence { get; set; }
	}
}
