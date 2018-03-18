namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection(Name = "some-root-name")]
	public interface IConfigInterfaceRootName
	{
		int Value { get; set; }
	}
}