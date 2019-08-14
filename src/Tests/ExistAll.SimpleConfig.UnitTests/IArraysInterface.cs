namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IArraysInterface
	{
		[ConfigProperty(DefaultValue = 4)]
		int Number { get; set; }

		[ConfigProperty(DefaultValue = new [] {"x", "y"})]
		string[] Array { get; set; }

		[ConfigProperty(DefaultValue = new [] {1, 1})]
		int[] Array2 { get; set; }
	}
}