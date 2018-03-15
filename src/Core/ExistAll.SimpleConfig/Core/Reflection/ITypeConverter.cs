using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal interface ITypeConverter
	{
		object ConvertValue(object value, PropertyInfo propertyInfo, ConfigOptions options);
	}
}