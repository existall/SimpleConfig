using System.Reflection;

namespace ExistAll.Settings
{
	public static class AssemblyCollectionExtensions
	{
		public static AssemblyCollection AddExportedAssembly(this AssemblyCollection collection, Assembly assembly)
		{
			collection.Add(new ExportedAssemblyHolder(assembly));
			return collection;
		}

		public static AssemblyCollection AddFullAssemblyHolder(this AssemblyCollection collection, Assembly assembly)
		{
			collection.Add(new FullAssemblyHolder(assembly));
			return collection;
		}
	}
}