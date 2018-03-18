namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection]
	public interface IArraysInterface
	{
		[DefaultValue(4)]
		int Number { get; set; }

		[DefaultValue("x", "y")]
		string[] Array { get; set; }

		[DefaultValue(1, 2)]
		int[] Array2 { get; set; }
	}
}