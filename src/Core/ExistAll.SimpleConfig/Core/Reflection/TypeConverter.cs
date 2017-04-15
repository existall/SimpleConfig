using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypeConverter : ITypeConverter
	{
		public object ConvertValue(string value, Type propertyType, ConfigOptions options)
		{
			if (value == null)
				return propertyType.GetTypeInfo().IsValueType ? Activator.CreateInstance(propertyType) : null;

			var strippedType = StripIfNullable(propertyType);

			if (strippedType.GetTypeInfo().IsEnum)
				return ConvertEnumType(strippedType, value);

			if (strippedType == typeof(DateTime))
				return ConvertFromDateTime(value, options);

			if (strippedType == typeof(Uri))
				return new Uri(value);

			if (strippedType.IsArray)
				return ConvertToArray(value, strippedType, options);

			return Convert.ChangeType(value, strippedType);
		}

		private object ConvertToArray(string value, Type strippedType, ConfigOptions options)
		{
			var strings = value.Split(new[] { options.ArraySplitDelimiter }, StringSplitOptions.RemoveEmptyEntries).ToArray();

			var objects = strings.Select(x => ConvertValue(x, strippedType.GetElementType(), options)).ToArray();

			var instance = Array.CreateInstance(strippedType.GetElementType(), objects.Length);

			for (var i = 0; i < instance.Length; i++)
			{
				instance.SetValue(objects[i], i);
			}

			return instance;
		}

		private static Type StripIfNullable(Type type)
		{
			return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) ?
				type.GetTypeInfo().GetGenericArguments()[0] :
				type;
		}

		private static DateTime ConvertFromDateTime(string value, ConfigOptions options)
		{
			return DateTime.ParseExact(value, options.DateTimeFormat, CultureInfo.InvariantCulture);
		}

		private static object ConvertEnumType(Type propertyType, object value)
		{
			return value.GetType() == propertyType ? value : Enum.Parse(propertyType, (string)value);
		}
	}
}
