namespace ExistAll.SimpleConfig.UnitTests
{
	public interface IRoot : IConfigSection
	{
		string Value { get; set; }
	}
}