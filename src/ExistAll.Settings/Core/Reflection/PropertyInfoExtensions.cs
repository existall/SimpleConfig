using System.Reflection;

namespace ExistAll.Settings.Core.Reflection
{
	internal static class PropertyInfoExtensions
	{
		public static object GetDefaultValue(this PropertyInfo property)
		{
			var defaultAttribute = property.GetCustomAttribute(typeof(IDefaultValueAttribute));

			return ((IDefaultValueAttribute) defaultAttribute)?.DefaultValue;
		}
	}
}
