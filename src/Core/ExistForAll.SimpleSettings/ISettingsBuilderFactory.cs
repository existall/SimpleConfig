namespace ExistForAll.SimpleSettings
{
    public interface ISettingsBuilderFactory
    {
        void AddSectionBinder(ISectionBinder sectionBinder);
        SettingsOptions Options { get; }
    }
}