using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypeConverter : ITypeConverter
	{
		public object ConvertValue(object value, Type propertyType, ConfigOptions options)
		{
			if (value == null)
				return propertyType.GetTypeInfo().IsValueType ? Activator.CreateInstance(propertyType) : null;

			var strippedType = StripIfNullable(propertyType);

			return options.Converters.First(x => x.CanConvert(strippedType)).Convert(value, strippedType);
		}

		private static Type StripIfNullable(Type type)
		{
			return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) ?
				type.GetTypeInfo().GetGenericArguments()[0] :
				type;
		}
	}
}
