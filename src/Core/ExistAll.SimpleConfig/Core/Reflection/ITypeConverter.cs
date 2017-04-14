using System;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal interface ITypeConverter
	{
		object ConvertValue(string value, Type propertyType, ConfigOptions options);
	}
}