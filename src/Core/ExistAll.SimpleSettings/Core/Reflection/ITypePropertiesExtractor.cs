using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal interface ITypePropertiesExtractor
	{
		IEnumerable<PropertyInfo> ExtractTypeProperties(Type type);
	}
}