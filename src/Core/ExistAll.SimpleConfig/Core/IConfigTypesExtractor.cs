using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core
{
	internal interface IConfigTypesExtractor
	{
		Type[] ExtractConfigTypes(IEnumerable<Assembly> assemblies, ConfigOptions options);
	}
}
