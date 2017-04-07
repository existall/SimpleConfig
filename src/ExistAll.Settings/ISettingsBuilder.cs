namespace ExistAll.Settings
{
	public interface ISettingsBuilder
	{
		void Add(ISectionBinder sectionBinder);
		ISettingsCollection Build(AssemblyCollection assemblies, SettingsOptions options);
	}
}