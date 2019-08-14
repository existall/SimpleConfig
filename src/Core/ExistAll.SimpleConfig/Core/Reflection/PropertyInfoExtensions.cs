using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal static class PropertyInfoExtensions
	{
		public static object GetDefaultValue(this PropertyInfo property)
		{
			var attribute = property.GetCustomAttribute<ConfigPropertyAttribute>();

			return attribute?.DefaultValue;
		}
	}
}
