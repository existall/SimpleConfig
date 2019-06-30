using System;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	public static class ConfigBuilderExtensions
	{
		public static IConfigCollection ScanAssemblies(this ConfigBuilder target, Assembly assembly, params Assembly[] assemblies)
		{
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            if (assemblies == null)
            {
                assemblies = new Assembly[0];
            }

            assemblies = assemblies.Concat(new[] { assembly }).ToArray();

            return target.ScanAssemblies(assemblies);
        }

        public static T GetConfig<T>(this ConfigBuilder target) where T : class
        {
            return (T) target.GetConfig(typeof(T));
        }
    }
}