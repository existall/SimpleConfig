using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleSettings.Core
{
	internal interface ISettingsTypesExtractor
	{
		Type[] ExtractSettingsTypes(IEnumerable<Assembly> assemblies, SettingsOptions options);
	}
}
