using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal interface ITypePropertiesExtractor
	{
		IEnumerable<PropertyInfo> ExtractTypeProperties(Type type);
	}
}