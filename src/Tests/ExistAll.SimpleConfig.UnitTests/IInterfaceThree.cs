namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection(Name = "dude")]
	public interface IInterfaceThree
	{
		int Age { get; set; }
	}
}