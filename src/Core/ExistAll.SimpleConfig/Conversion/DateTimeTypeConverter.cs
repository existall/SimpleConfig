using System;
using System.Globalization;

namespace ExistAll.SimpleConfig.Conversion
{
	internal class DateTimeTypeConverter : IConfigTypeConverter
	{
		private readonly ConfigOptions _configOptions;

		public DateTimeTypeConverter(ConfigOptions configOptions)
		{
			_configOptions = configOptions;
		}

		public bool CanConvert(Type configType)
		{
			return configType == typeof(DateTime);
		}

		public object Convert(object value, Type configType)
		{
			return DateTime.ParseExact((string)value, _configOptions.DateTimeFormat, CultureInfo.InvariantCulture);
		}
	}
}