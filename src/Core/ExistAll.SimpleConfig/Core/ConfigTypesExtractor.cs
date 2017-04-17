using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core
{
	internal class ConfigTypesExtractor : IConfigTypesExtractor
	{
		public Type[] ExtractConfigTypes(IEnumerable<Assembly> assemblies, ConfigOptions options)
		{
			if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
			if (options == null) throw new ArgumentNullException(nameof(options));

			return assemblies.SelectMany(x=>x.GetExportedTypes())
				.Where(x => x.GetTypeInfo().IsInterface && IsFromOptions(x, options))
				.ToArray();
		}

		private static bool IsFromOptions(Type type, ConfigOptions options)
		{
			try
			{
				var info = type.GetTypeInfo();

				if (info.GetCustomAttribute(options.AttributeType, true) != null)
					return true;

				if (options.InterfaceBase.GetTypeInfo().IsAssignableFrom(info))
					return true;

				if (info.Name.ToLower().EndsWith(options.ConfigSuffix.Trim().ToLower()))
					return true;

				return false;
			}
			catch (Exception e)
			{
				throw new ConfigExtractionException(type,e);
			}
		}
	}
}