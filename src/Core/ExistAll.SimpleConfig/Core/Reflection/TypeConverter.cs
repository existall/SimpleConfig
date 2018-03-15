using System;
using System.Linq;
using System.Reflection;
using ExistAll.SimpleConfig.Convertion;

namespace ExistAll.SimpleConfig.Core.Reflection
{
	internal class TypeConverter : ITypeConverter
	{
		public object ConvertValue(object value, PropertyInfo propertyInfo, ConfigOptions options)
		{
			var propertyType = propertyInfo.PropertyType;

			if (value == null)
				return propertyType.GetTypeInfo().IsValueType ? Activator.CreateInstance(propertyType) : null;

			var strippedType = StripIfNullable(propertyType);

			var configTypeConverter = GetConverter(strippedType, propertyInfo, options);

			return configTypeConverter.Convert(value, strippedType);
		}

		private IConfigTypeConverter GetConverter(Type strippedType, PropertyInfo propertyInfo, ConfigOptions options)
		{
			var attribute = propertyInfo
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
