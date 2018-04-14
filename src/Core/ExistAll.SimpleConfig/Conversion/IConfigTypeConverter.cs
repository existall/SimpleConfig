using System;

namespace ExistAll.SimpleConfig.Conversion
{
	public interface IConfigTypeConverter
	{
		bool CanConvert(Type configType);
		object Convert(object value, Type configType);
	}
}