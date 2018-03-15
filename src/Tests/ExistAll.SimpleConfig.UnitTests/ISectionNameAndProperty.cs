namespace ExistAll.SimpleConfig.UnitTests
{
	[ConfigSection(Name = "section-root")]
	public interface ISectionNameAndProperty
	{
		[ConfigProperty(Name = "section-value")]
		string Value { get; set; }
	}
}