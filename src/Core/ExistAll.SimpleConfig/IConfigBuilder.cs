namespace ExistAll.SimpleConfig
{
	public interface IConfigBuilder
	{
		void Add(ISectionBinder sectionBinder);
		IConfigCollection Build(AssemblyCollection assemblies, ConfigOptions options);
	}
}