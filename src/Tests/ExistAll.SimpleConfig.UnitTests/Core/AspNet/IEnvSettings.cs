using ExistAll.SimpleConfig.Core.AspNet;

namespace ExistAll.SimpleConfig.UnitTests.Core.AspNet
{
	[ConfigSection]
	public interface IEnvSettings
	{
		[DevelopmentDefaultValue(Environments.Development)]
		[StagingDefaultValue(Environments.Staging)]
		[ProductionDefaultValue(Environments.Production)]
		string Value { get; set; }
	}
}
