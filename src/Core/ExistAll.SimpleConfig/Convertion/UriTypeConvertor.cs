using System;

namespace ExistAll.SimpleConfig.Convertion
{
	internal class UriTypeConvertor : IConfigTypeConverter
	{
		public bool CanConvert(Type configType)
		{
			return configType == typeof(Uri);
		}

		public object Convert(object value, Type configType)
		{
			return new Uri((string)value);
		}
	}
}