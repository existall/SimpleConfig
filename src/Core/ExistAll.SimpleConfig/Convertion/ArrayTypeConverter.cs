using System;
using System.Linq;

namespace ExistAll.SimpleConfig.Convertion
{
	internal class ArrayTypeConverter : IConfigTypeConverter
	{
		private readonly ConfigOptions _configOptions;
		private readonly TypeConvertersCollections _converters;

		public ArrayTypeConverter(ConfigOptions configOptions, TypeConvertersCollections converters)
		{
			_configOptions = configOptions;
			_converters = converters;
		}

		public bool CanConvert(Type configType)
		{
			return configType.IsArray;
		}

		public object Convert(object value, Type configType)
		{
			if (value is string stringArray)
			{
				value = stringArray.Split(new[] {_configOptions.ArraySplitDelimiter}, StringSplitOptions.RemoveEmptyEntries)
					.ToArray();
			}

			var values = value.GetType().IsArray ? (object[]) value : new[] {value};

			var elementType = configType.GetElementType();

			var configTypeConverter = _converters.First(x => x.CanConvert(elementType));

			var objects = values.Select(x => configTypeConverter.Convert(x, elementType)).ToArray();

			var instance = Array.CreateInstance(configType.GetElementType(), objects.Length);

			for (var i = 0; i < instance.Length; i++)
			{
				instance.SetValue(objects[i], i);
			}

			return instance;
		}
	}
}