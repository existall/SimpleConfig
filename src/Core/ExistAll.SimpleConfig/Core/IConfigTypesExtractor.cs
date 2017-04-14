using System;

namespace ExistAll.SimpleConfig.Core
{
	internal interface IConfigTypesExtractor
	{
		Type[] ExtractConfigTypes(AssemblyCollection assemblies, ConfigOptions options);
	}
}
