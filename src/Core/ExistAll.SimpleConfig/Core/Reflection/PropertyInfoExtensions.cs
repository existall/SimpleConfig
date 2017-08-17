using System.Collections;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal static class PropertyInfoExtensions
	{
		public static object GetDefaultValue(this PropertyInfo property)
		{
			var attributes = property.GetCustomAttributes<ConditionalDefaultValueBaseAttribute>();

			return attributes.FirstOrDefault(x => x.ShouldUse)?.DefaultValue;
		}

		public static bool TryGetEnvironmentVariableAttributeValue(this PropertyInfo property,
			IDictionary variables,
			ConfigOptions configOptions,
			out object result)
		{
			result = null;

			var attribute = property.GetCustomAttribute(typeof(EnvironmentVariableBaseAttribute));

			var variable = ((EnvironmentVariableBaseAttribute)attribute)?.Variable;

			if (variable == null)
				return false;
			
			result =  (string) variables[variable];

			return true;
		}
	}
}
