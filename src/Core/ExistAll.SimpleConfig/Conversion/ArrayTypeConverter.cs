using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleConfig.Conversion
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
                value = stringArray.Split(new[] {_configOptions.ArraySplitDelimiter},
                        StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();
            }

            var values = value.GetType().IsArray ? (IEnumerable) value : new[] {value};

            var elementType = configType.GetElementType();

            var configTypeConverter = _converters.First(x => x.CanConvert(elementType));

            var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (var item in values)
            {
                var convertedValue = configTypeConverter.Convert(item, elementType);
                list.Add(convertedValue);
            }

            var toArray = typeof(Enumerable).GetTypeInfo()
                .GetMethod("ToArray")
                .MakeGenericMethod(elementType);

            var array = toArray.Invoke(null, new object[] {list});

            return array;
        }
    }
}