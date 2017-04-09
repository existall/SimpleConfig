using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ExistAll.Settings.Core.Reflection
{
	internal interface ITypeConverter
	{
		object ConvertValue(object value, Type propertyType);
	}

	internal class TypeConverter : ITypeConverter
	{
		public object ConvertValue(object value, Type propertyType)
		{
			if (value == null)
				return propertyType.GetTypeInfo().IsValueType ? Activator.CreateInstance(propertyType) : null;

			var strippedType = StripNullable(propertyType);

			if (strippedType == typeof(Uri))
				return new Uri((string)value);

			if (strippedType == typeof(DateTime))
				return ConvertFromDateTime(value);

			if (strippedType.GetTypeInfo().IsEnum)
				return ConvertEnumType(strippedType, value);

			if (strippedType.IsArray)
				return ConvertToArray(value, strippedType);

			return Convert.ChangeType(value, strippedType);
		}

		private static object ConvertToArray(object value, Type strippedType)
		{
			var values = value.GetType().IsArray ? (object[])value : new[] { value };

			var objects = values.Select(x => ConvertValue(x, strippedType.GetElementType())).ToArray();

			var instance = Array.CreateInstance(strippedType.GetElementType(), objects.Length);

			for (var i = 0; i < instance.Length; i++)
			{
				instance.SetValue(objects[i], i);
			}

			return instance;
		}

		private static Type StripNullable(Type type)
		{
			return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) ?
				type.GetTypeInfo().GetGenericArguments()[0] :
				type;
		}

		private static DateTime ConvertFromDateTime(object value)
		{
			return DateTime.ParseExact((string)value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		private static object ConvertEnumType(Type propertyType, object value)
		{
			if (value.GetType() == propertyType)
				return value;

			return Enum.Parse(propertyType, (string)value);
		}
	}
}
