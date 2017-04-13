using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public static class AssemblyCollectionExtensions
	{
		public static AssemblyCollection AddExportedTypesAssembly(this AssemblyCollection collection, Assembly assembly)
		{
			collection.Add(new ExportedAssemblyHolder(assembly));
			return collection;
		}

		public static AssemblyCollection AddFullTypesAssembly(this AssemblyCollection collection, Assembly assembly)
		{
			collection.Add(new FullAssemblyHolder(assembly));
			return collection;
		}
	}
}