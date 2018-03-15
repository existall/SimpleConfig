using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public static class ConfigBuilderExtensions
	{
		public static IConfigCollection Build(this ConfigBuilder target, Assembly assembly, params Assembly[] assemblies)
		{
			var assemblyList = new List<Assembly>(assemblies) {assembly};
			return target.Build(assemblyList);
		}
	}
}