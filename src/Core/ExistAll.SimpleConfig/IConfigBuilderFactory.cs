namespace ExistAll.SimpleConfig
{
    public interface IConfigBuilderFactory
    {
        IConfigBuilderFactory AddSectionBinder(ISectionBinder sectionBinder);
        ConfigOptions Options { get; }
    }
}