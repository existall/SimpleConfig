using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExistAll.SimpleConfig.Core.Reflection;

namespace ExistAll.SimpleConfig.Conversion
{
	internal class EnumerableTypeConverter : IConfigTypeConverter
	{
		private readonly ConfigOptions _configOptions;
		private readonly TypeConvertersCollections _converters;

		public EnumerableTypeConverter(ConfigOptions configOptions, TypeConvertersCollections converters)
		{
			_configOptions = configOptions;
			_converters = converters;
		}

		public bool CanConvert(Type configType)
		{
			return configType.IsEnumerable();
		}

		public object Convert(object value, Type configType)
		{
			if (value is string stringArray)
			{
				value = stringArray.Split(new[] { _configOptions.ArraySplitDelimiter }, StringSplitOptions.RemoveEmptyEntries)
					.ToArray();
			}

			var values = value.GetType().IsArray ? (IEnumerable)value : new[] { value };

			var elementType = configType.GetTypeInfo().GetGenericArguments().First();

			var configTypeConverter = _converters.First(x => x.CanConvert(elementType));

			var instance = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
			
			foreach (var item in values)
			{
				var convertedValue = configTypeConverter.Convert(item, elementType);
				instance.Add(convertedValue);
			}

			return instance;
		}
	}
}