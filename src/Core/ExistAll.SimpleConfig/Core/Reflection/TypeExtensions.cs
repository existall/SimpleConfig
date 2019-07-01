using System;
using System.Collections.Generic;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal static class TypeExtensions
	{
		public static string GetNormalizeInterfaceName(this Type target)
		{
			return target.Name[0] == 'I' ? $"{target.Name.Substring(1)}" : target.Name;
		}

		public static string GetSectionName(this Type configClass, ConfigOptions options)
		{
			var attribute = configClass.GetTypeInfo().GetCustomAttribute<ConfigSectionAttribute>(true);

			return !string.IsNullOrWhiteSpace(attribute?.Name)
				? attribute.Name
				: options.SectionNameFormatter(configClass);
		}

		public static string GetPropertyName(this PropertyInfo propertyInfo)
		{
			var attribute = propertyInfo.GetCustomAttribute<ConfigPropertyAttribute>(true);

			return !string.IsNullOrWhiteSpace(attribute?.Name)
				? attribute.Name
				: propertyInfo.Name;
		}

		public static bool IsEnumerable(this Type type)
		{
			var info = type.GetTypeInfo();

			return info.IsGenericType && info.GetGenericTypeDefinition() == typeof(IEnumerable<>);
		}
	}
}