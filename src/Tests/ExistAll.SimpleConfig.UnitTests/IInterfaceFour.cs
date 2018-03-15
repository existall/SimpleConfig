namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection(Name = "father")]
	public interface IInterfaceFour
	{
		[ConfigProperty(Name = "mother")] int Age { get; set; }
	}
}