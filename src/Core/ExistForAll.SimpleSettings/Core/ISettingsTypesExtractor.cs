using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistForAll.SimpleSettings.Core
{
	internal interface ISettingsTypesExtractor
	{
		Type[] ExtractSettingsTypes(IEnumerable<Assembly> assemblies, SettingsOptions options);
	}
}
