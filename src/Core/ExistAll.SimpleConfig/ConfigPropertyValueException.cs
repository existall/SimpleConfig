using System;
using System.Reflection;

namespace ExistAll.SimpleConfig
{
	internal class ConfigPropertyValueException : Exception
	{
		public ConfigPropertyValueException(string value, PropertyInfo property, Exception exception)
			: base(Resources.PropertySetterExceptionMessage(value, property), exception)
		{

		}
	}
}