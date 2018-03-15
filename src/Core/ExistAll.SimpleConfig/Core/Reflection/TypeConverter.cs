using System;
using System.Linq;
using System.Reflection;
using ExistAll.SimpleConfig.Convertion;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypeConverter : ITypeConverter
	{
		public object ConvertValue(object value, Type propertyType, ConfigOptions options)
		{
			if (value == null)
				return propertyType.GetTypeInfo().IsValueType ? Activator.CreateInstance(propertyType) : null;

			var strippedType = StripIfNullable(propertyType);

			var configTypeConverter = GetConverter(strippedType, propertyType, options);

			return configTypeConverter.Convert(value, strippedType);
		}

		private IConfigTypeConverter GetConverter(Type strippedType, Type propertyType, ConfigOptions options)
		{
			var attribute = propertyType.GetTypeInfo()
				.GetCustomAttribute<ConfigPropertyAttribute>();

			if (attribute?.ConvertorType == null)
				return options.Converters.First(x => x.CanConvert(strippedType));

			var converter = (IConfigTypeConverter)Activator.CreateInstance(attribute.ConvertorType);

			return converter;
		}

		private static Type StripIfNullable(Type type)
		{
			return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) ?
				type.GetTypeInfo().GetGenericArguments()[0] :
				type;
		}
	}
}
