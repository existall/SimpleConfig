using System;

namespace ExistAll.SimpleConfig.Core
{
	internal interface IConfigTypesExtractor
	{
		Type[] ExtractConfigTypes(IAssemblyCollection assemblies, ConfigOptions options);
	}
}
