using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleSettings.Core.Reflection
{
	internal static class PropertyInfoExtensions
	{
		public static object GetDefaultValue(this PropertyInfo property)
		{
			var attribute = property.GetCustomAttribute<SettingsPropertyAttribute>();

			return attribute?.DefaultValue;
		}
	}
}
