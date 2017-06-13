using System;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal interface ITypeConverter
	{
		object ConvertValue(object value, Type propertyType, ConfigOptions options);
	}
}