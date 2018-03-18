using System;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	internal class ConfigPropertyValueException : Exception
	{
		public ConfigPropertyValueException(
			Type interfaceType,
			object value,
			PropertyInfo property,
			Exception exception)
			: base(Resources.PropertySetterExceptionMessage(interfaceType, value, property), exception)
		{}
	}
}