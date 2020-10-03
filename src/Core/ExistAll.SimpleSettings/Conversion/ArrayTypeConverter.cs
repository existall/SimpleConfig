using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExistAll.SimpleSettings.Conversion
{
    internal class ArrayTypeConverter : ISettingsTypeConverter
    {
        private readonly SettingsOptions _settingsOptions;
        private readonly TypeConvertersCollections _converters;

        public ArrayTypeConverter(SettingsOptions settingsOptions, TypeConvertersCollections converters)
        {
            _settingsOptions = settingsOptions;
            _converters = converters;
        }

        public bool CanConvert(Type settingsType)
        {
            return settingsType.IsArray;
        }

        public object Convert(object value, Type configType)
        {
            if (value is string stringArray)
            {
                value = stringArray.Split(new[] {_settingsOptions.ArraySplitDelimiter},
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