using System;
using System.Collections.Generic;
using System.Reflection;
using ExistAll.SimpleConfig.Conversion;

namespace ExistAll.SimpleConfig
{
	public static class ConfigBuilderExtensions
	{
		public static ConfigBuilder AddAssemblies(this ConfigBuilder target, Assembly assembly, params Assembly[] assemblies)
		{
			target.AddAssembly(assembly);
			
			foreach (var item in assemblies)
			{
				target.AddAssembly(item);	
			}
			
			return target;
		}

		public static ConfigBuilder AddAssemblies(this ConfigBuilder target, IEnumerable<Assembly> assemblies)
		{
			if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
			
			foreach (var assembly in assemblies)
			{
				target.AddAssembly(assembly);	
			}
			
			return target;
		}
		
		public static ConfigBuilder AddTypeConverter(this ConfigBuilder target, IConfigTypeConverter configTypeConverter)
		{
			if (configTypeConverter == null) throw new ArgumentNullException(nameof(configTypeConverter));
			target.Options.Converters.AddFirst(configTypeConverter);
			return target;
		}
	}
}