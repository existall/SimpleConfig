namespace ExistAll.SimpleConfig
{
    public interface IConfigBuilderFactory
    {
        void AddSectionBinder(ISectionBinder sectionBinder);
        ConfigOptions Options { get; }
    }
}