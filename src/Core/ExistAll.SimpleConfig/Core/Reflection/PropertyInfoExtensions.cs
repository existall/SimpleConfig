using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal static class PropertyInfoExtensions
	{
		public static object GetDefaultValue(this PropertyInfo property)
		{
			var defaultAttribute = property.GetCustomAttribute(typeof(DefaultValueBaseAttribute));

			return ((DefaultValueBaseAttribute) defaultAttribute)?.DefaultValue;
		}
	}
}
