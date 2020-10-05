using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistForAll.SimpleSettings.Core.Reflection
{
	internal interface ITypePropertiesExtractor
	{
		IEnumerable<PropertyInfo> ExtractTypeProperties(Type type);
	}
}