using System;
using System.Reflection;

namespace ExistAll.Settings
{
	internal class SettingsPropertyValueException : Exception
	{
		public SettingsPropertyValueException(string value, PropertyInfo property, Exception exception)
			: base(Resources.PropertySetterExceptionMessage(value, property), exception)
		{

		}
	}
}